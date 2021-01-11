Title: Controller Controller where art thou Controller?  (Feature Controller Factory)
Tags: 
  - C#
Author: Steven T. Cramer

---

#Goal:
Move an existing MVC site from layers to features without breaking everything along the way.

###Default MVC Controller location
The default locations for ASP.net MVC5 items are as follows:

![](/content/images/2016/08/2016-08-13_1255-2.png)

All Controllers are located in a `Controllers` folder and must end with the name `Controller`.

###Feature Controller location
When we move to the [features method](http://www.thefreezeteam.com/2015/08/10/building-mvc-jimmy-style/) we would prefer our controller to be an embedded class in the Feature and always named `FeatureController`.  Thus the full name of the controller type would be: 

`<App>.Features.<Route>.<FeatureName>+FeatureController`

example:  

* App = `WebApplication1`
* Route= `Route`
* FeatureName = `Feature`
* Result: `WebApplication1.Features.Route.Feature+FeatureController`

![](/content/images/2016/11/2016-11-03_0521.png)

We need MVC to be able to find the new feature controller location as well as the default location.  This facilitates a migration from one to the other.

###Create test feature
This post is not about creating features.  See [Building MVC Jimmy Style ](http://www.thefreezeteam.com/2015/08/10/building-mvc-jimmy-style/) for more details  until the updated blog series is released.  The below test feature will suffice for the purpose at hand.

In order to test our Features Controller, let's create a simple Feature.

First, create a `Features` folder at the root of the project and within it create a `Route` Folder and within that create a `Feature` class as follows:

```csharp
namespace WebApplication1.Features.Route
{
  using System.Web.Mvc;

  public class Feature
  {
    public class FeatureController : Controller
    {
      // URL = http://localhost:23187/Route/Feature
      public ActionResult Get()
      {
        return Content("Get Action");
      }

      public ActionResult Post()
      {
        return Content("Post Action");
      }
    }
  }
}

```
It should be noted that in the TheFreezeTeam MVC Feature Pattern we have a one to one relationship between a URL and a Controller.  All Actions are named according to the HTTP method (Get, Post, Put, Delete, etc...) they implement. 

### DefaultControllerFactory
In MVC5 controllers are created using the `DefaultControllerFactory.CreateController` method which uses a naming convention of `<controllerName>Controller` to get the controller type via the `GetControllerType` method.  

### ControllerFactory
We want to support the original convention as well as our feature based convention.  To do this we first Create a new `ControllerFactory` that inherits from `DefaultControllerFactory`.

Create an `Infrastructure folder` and inside it create a new class called ControllerFactory.

![](/content/images/2016/08/2016-08-21_1203.png)

Override the `GetControllerType` method:

```csharp
namespace WebApplication1.Infrastructure
{
  using System;
  using System.Web.Mvc;
  using System.Web.Routing;

  public class ControllerFactory : DefaultControllerFactory
  {
    protected override Type GetControllerType(RequestContext requestContext, string controllerName)
    {
      return base.GetControllerType(requestContext, controllerName);
    }
  }
}
```
Given we just call the base method we expect nothing to currently change.

To use this new `ControllerFactory` we need to edit the `Global.asax` file by adding 

```csharp
      //Replace Default Controller factor with Feature version
      ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
```

Now set a breakpoint in `GetControllerType` and verify that the breakpoint is hit when executing the app.

Update the ControllerFactory as follows:

```csharp
  public class ControllerFactory : DefaultControllerFactory
  {
    const string BaseFeatureNamespace = "WebApplication1.Features";
    const string FeatureControllerName = "FeatureController";
    protected override Type GetControllerType(RequestContext aRequestContext, string aRoute)
    {
      Type type = base.GetControllerType(aRequestContext, aRoute);
      if (type == null)      {
        string Feature = aRequestContext.RouteData.GetRequiredString(valueName: "action");
        string typeName = $"{BaseFeatureNamespace}.{aRoute}.{Feature}+{FeatureControllerName}";

        System.Reflection.Assembly assembly = typeof(ControllerFactory).Assembly;
        type = assembly.GetType(typeName);
      }
      return type;
    }
```

###FeatureActionInvoker
This will build up the proper name for the controller and use assembly reflection to get the Type. Yet it will not find the feature actions.  To fix this we need to replace the default ActionInvoker with our `FeatureActionInvoker`

```csharp
public class FeatureActionInvoker : AsyncControllerActionInvoker
    {
      protected override ActionDescriptor FindAction(ControllerContext aControllerContext, ControllerDescriptor aControllerDescriptor, string aActionName)
      {
        ActionDescriptor actionDescriptor = base.FindAction(aControllerContext, aControllerDescriptor, actionName: aActionName);
        if (actionDescriptor == null)
        {
          actionDescriptor = base.FindAction(aControllerContext, aControllerDescriptor, actionName: aControllerContext.HttpContext.Request.HttpMethod);
        }
        return actionDescriptor;
      }
    }
```
This uses the base method to attempt to find the Action to invoke but if it is null will look for a Feature Action based on the HttpMethod naming convention.  

To use this new `FeatureActionInvoker`  we need to replace the default one on the controller.

We do this by overriding the `ControllerFactory.CreateController` method with:

```csharp
    public override IController CreateController(RequestContext aRequestContext, string aControllerName)
    {
      IController controller = base.CreateController(aRequestContext, aControllerName);
      return ReplaceActionInvoker(controller);
    }

    private IController ReplaceActionInvoker(IController aController)
    {
      var mvcController = aController as Controller;
      if (mvcController != null)
      {
        mvcController.ActionInvoker = new FeatureActionInvoker();
      }

      return aController;
    }
```

This should now function for both the default and the TFT feature-based method.

Test that the home URL (http://localhost:23187/) and the feature based URL (http://localhost:23187/Route/Feature) to confirm that both work correctly.

Final source for the `ControllerFactory` below:

```csharp
namespace WebApplication1.Infrastructure
{
  using System;
  using System.Web.Mvc;
  using System.Web.Mvc.Async;
  using System.Web.Routing;

  public class ControllerFactory : DefaultControllerFactory
  {
    const string BaseFeatureNamespace = "WebApplication1.Features";
    const string FeatureControllerName = "FeatureController";
    protected override Type GetControllerType(RequestContext aRequestContext, string aRoute)
    {
      Type type = base.GetControllerType(aRequestContext, aRoute);
      if (type == null)      {
        string Feature = aRequestContext.RouteData.GetRequiredString(valueName: "action");
        string typeName = $"{BaseFeatureNamespace}.{aRoute}.{Feature}+{FeatureControllerName}";

        System.Reflection.Assembly assembly = typeof(ControllerFactory).Assembly;
        type = assembly.GetType(typeName);
      }
      return type;
    }
    public override IController CreateController(RequestContext aRequestContext, string aControllerName)
    {
      IController controller = base.CreateController(aRequestContext, aControllerName);
      return ReplaceActionInvoker(controller);
    }

    private IController ReplaceActionInvoker(IController aController)
    {
      var mvcController = aController as Controller;
      if (mvcController != null)
      {
        mvcController.ActionInvoker = new FeatureActionInvoker();
      }

      return aController;
    }


    public class FeatureActionInvoker : AsyncControllerActionInvoker
    {
      protected override ActionDescriptor FindAction(ControllerContext aControllerContext, ControllerDescriptor aControllerDescriptor, string aActionName)
      {
        ActionDescriptor actionDescriptor = base.FindAction(aControllerContext, aControllerDescriptor, actionName: aActionName);
        if (actionDescriptor == null)
        {
          actionDescriptor = base.FindAction(aControllerContext, aControllerDescriptor, actionName: aControllerContext.HttpContext.Request.HttpMethod);
        }
        return actionDescriptor;
      }
    }
  }
}
```

Full sample solution can be found on GitHub @:

https://github.com/StevenTCramer/FeatureController



