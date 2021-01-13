Title: Creating base Jimmy MVC5 React Project.
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

# The architecture

The client side is going to use React via TypeScript and the server is going to expose a CRUD API using C#, ASP.net MVC5, MediatR, StructureMap, and Automapper. Server-side integration testing will be done with Fixie.  This is based directly on [Jimmy Bogard's blogs](https://lostechies.com/) sample code and anything else I can find from him or the people at Headspring.  Instead of using HTMLTags we will use React.

# Create the project

Start with blank MVC application.  I don't want bootstrap jquery etc that are included by default in the MVC template. We will start with the Empty template and add what we need.

Check the MVC box and it will create the minimum folder structure.

![](/content/images/2016/06/2016-06-10--2-.png)

![](/content/images/2016/06/EmptyMvcTemplate.png)

Update the default namespace for the project to <Organization>.FlashCards Tft.FlashCards in this case.

Update the Nuget packages.  

![](/content/images/2016/06/UpdateNugetPackages.png)

![](/content/images/2016/06/OriginalSolution.png)

#Let's make something work the old way.

## Add a controller

![](/content/images/2016/06/EmptyController.png)

Leave it named DefaultController

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tft.FlashCards.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

The solution will have also added the `Default` folder under the Views folder.

![](/content/images/2016/06/SolutionWithDefaultController.png)
#### Add a view

![](/content/images/2016/06/AddView.png)

![](/content/images/2016/06/AddViewDetails.png)

```
@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div> 
      Hello
    </div>
</body>
</html>
```

Run the application http://localhost:xxxx/Default/Index 
and confirm you can see `Hello`

#### Views to Features

Rename the `Views` Folder to `Features`

Running the application now will give you an error.

```
The view 'Index' or its master was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/Default/Index.aspx
~/Views/Default/Index.ascx
~/Views/Shared/Index.aspx
~/Views/Shared/Index.ascx
~/Views/Default/Index.cshtml
~/Views/Default/Index.vbhtml
~/Views/Shared/Index.cshtml
~/Views/Shared/Index.vbhtml
```

To fix this we change the view engine to one that looks in the proper location:

Create a new root folder named Infrastructure 
![](/content/images/2016/06/InfrastructerFolder.png)
Inside the Infrastructure folder Create new class named: FeatureRazorViewEngine

```
namespace Tft.FlashCards.Infrastructure
{
	using System.Web.Mvc;

	public class FeatureRazorViewEngine : RazorViewEngine
	{
		public FeatureRazorViewEngine()
		{
			ViewLocationFormats = new[]
			{
				"~/Features/{1}/{0}.cshtml",
				"~/Features/{1}/{0}.vbhtml",
				"~/Features/Shared/{0}.cshtml",
				"~/Features/Shared/{0}.vbhtml",
			};

			MasterLocationFormats = ViewLocationFormats;
			PartialViewLocationFormats = ViewLocationFormats;
		}
	}
}
```

To use the new ViewEngine, update the Global.asax.cs as follows:

```
namespace Tft.FlashCards
{
	using Infrastructure;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// Replace Default ViewEngine with Feature version
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new FeatureRazorViewEngine());
		}
	}
}
```
You application will now function using the "Feature" folder instead of the "View" folder.

## Removing the Controllers Folder

Move DeafultController.cs to the Features\Default directory and delete the Controllers folder.

Using the feature concept we no longer have to indicate the purpose of the controller by calling it `<Default>Controller`. Given it is in the `Default` namespace the prefix becomes redundant.

Rename DefaultController.cs to FeatureController.cs and the class name to FeatureController and update the namespace inside the file as:

```
namespace Tft.FlashCards.Features.Default
{
	using System.Web.Mvc;

	public class FeatureController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}
```

Running the application will now give an error:

```
Server Error in '/' Application.

The resource cannot be found.
```

The url format is set in the RouteConfig as follows: (Notice this is the default and is not changed.)

```
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
```

MVC controllers are created using the `DefaultControllerFactory.CreateController` and it uses a naming convention of `<controllerName>Controller` to get the controller type. This will no longer work with our new feature based naming convention.

To fix this create your own controller factory class in the `Infrastructure` Folder as follows:

```
namespace Tft.FlashCards.Infrastructure
{
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class ControllerFactory : DefaultControllerFactory
	{
		protected override Type GetControllerType(RequestContext aRequestContext, string aControllerName)
		{
			string action = aRequestContext.RouteData.GetRequiredString(valueName: "action");
			string typeName = "FlashCards.Features." + aControllerName + ".FeatureController";

			System.Reflection.Assembly assembly = typeof(ControllerFactory).Assembly;
			Type type = assembly.GetType(typeName);
			return type;
		}
	}
}
```
To use this controller factory set it directly in the `Global.asax.cs` file using:
```
ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
```

Later after we add an IoC container this will need to be removed.

At this point the Global.asax.cs file should be as follows:

```
namespace Tft.FlashCards
{
	using Infrastructure;
	using System.Web.Mvc;
	using System.Web.Routing;

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// Replace Default ViewEngine with Feature version
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new FeatureRazorViewEngine());

			//Replace Default Controller factor with Feature version
			ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());

		}
	}
}
```
The application should now run.

## Create the first Feature

A Feature has a single URL and single controller.  A feature can return HTML or JSON or any mime type you like.

Our first feature is going to be the start page for a "Single Page Application". This Feature will return a basic HTML page that links in the React javascript.

Under the `Features` folder, create a `FlashCardApplication` Folder

Inside the `FlashCardApplication` folder create a new class named `FlashCardApplicationPage` this will contain the features logic.

Add a "MVC 5 View Page (Razor)" to the `FlashCardApplication` folder named `FlashCardApplicationPage`.

Nest the `FlashCardApplicationPage.cshtml` file under the FlashCardApplicationPage.cs file to keep the solution organized, using the [File Nesting](https://visualstudiogallery.msdn.microsoft.com/3ebde8fb-26d8-4374-a0eb-1e4e2665070c) add-in.

Update the default `FlashCardApplicationPage.cshtml` code with the following so we can see something when the view renders. 
```
@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Flash Card Application</title>
</head>
<body>
    <div>
			Hello World
    </div>
</body>
</html>

```
## Add a Controller to the Feature

Nested inside the `FlashCardApplicationPage` class add a Feature Controller.

Given each feature now represents a single URL the name of the controller method no longer needs to correspond to the URL as there can only be one method for each HTTP type `get`, `post`, etc... on each controller. So we will name all of our methods "Action", and thus give us the opportunity to be more DRY. 

The FlashCardApplicationPage.cs file should be as follows:

```
namespace Tft.FlashCards.Features.FlashCardApplication
{
	using System.Web.Mvc;
	public class FlashCardApplicationPage
	{
		public class FeatureController : Controller
		{
			[HttpGet]
			public ActionResult Action()
			{
				return View();
			}
		}
	}
}
```

The desired URL of this Feature is /FlashCardApplication/FlashCardApplicationPage.

Execute the app and navigate to the desired URL.

You will get `The resource cannot be found.` error. This is because the controller can not be found using our Controller Factory. The current factory is looking for a controller of type "FlashCards.Features.FlashCardApplication.FeatureController" and a method named "FlashCardApplicationPage".

Our controller's fully qualified name is "FlashCards.Features.FlashCardApplication.FlashCardApplicationPage+FeatureController" because we have embedded it inside the feature. So we either have to change the factory, or not embed the controller. Having done this both ways I chose to embed the controller as this allows for a more DRY solution. Also, the method we are looking for is always named `Action` and we will address this also in the controller factory.

```
namespace Tft.FlashCards.Infrastructure
{
	using System;
	using System.Web.Mvc;
	using System.Web.Mvc.Async;
	using System.Web.Routing;

	public class ControllerFactory : DefaultControllerFactory
	{
		const string FeaturesFolderName = "Features";
		const string FeatureControllerName = "FeatureController";
		const string FeatureControllerActionName = "Action";

		protected override Type GetControllerType(RequestContext aRequestContext, string aControllerName)
		{
			string baseNamespace = typeof(ControllerFactory).Namespace.Split('.')[0];
			string action = aRequestContext.RouteData.GetRequiredString(valueName: "action");
			string typeName = $"{baseNamespace}.{FeaturesFolderName}.{aControllerName}.{action}+{FeatureControllerName}";

			System.Reflection.Assembly assembly = typeof(ControllerFactory).Assembly;
			Type type = assembly.GetType(typeName);
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
				return base.FindAction(aControllerContext, aControllerDescriptor, actionName: FeatureControllerActionName);
			}
		}
	}
}
```
Note: you could support multiple methods for finding the ControllerType.
Executing  the application should now work.

# Ensure Typescript
Make sure you have installed TypeScript 1.8.4 in Visual studio.
![](/content/images/2016/04/2016-04-09_1405.png)

#### Install React.Web.Mvc4 Nuget

![](/content/images/2016/04/2016-04-09_1437.png)

`install-package React.Web.Mvc4`

Take note of all the dependencies that will also be installed.
![](/content/images/2016/06/ReactNugetDependencies.png)
After installation run updates on other NuGet packages.

Note that this package attempts to modify the web.config file in the Views folder but that folder no longer exists so we need to modify the web.config manually and then delete the Views folder.

Update the `web.config` file in the Features folder to include
`<add namespace="React.Web.Mvc" />` as follows:

```
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.3.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Routing" />
        <add namespace="Tft.FlashCards" />
        <add namespace="React.Web.Mvc" />
      </namespaces>
    </pages>
  </system.web.webPages.razor>
```
# Create Typescript Client Application Object

Create a top-level application object for the client side code.

Right click on the `FlashCardApplication` folder and select Add->TypeScript JSX File

![](/content/images/2016/06/FlashCardsApplicationJsx.png)

After adding the file the following Dialog will appear:

![](/content/images/2016/06/TypesScriptConfigured.png)

This reminds us we need to add the React typings but that isn't as easy as it should be just select no for now.

Go to the Properties on your project and select the `TypeScript Build` tab and for the `JSX compilation in TSX Files` and select `React`

![](/content/images/2016/04/2016-04-09_1514.png)
### Add lodash NuGet and Typings
We add lodash first because it will create the proper folders for us and we will want it anyway. 

![](/content/images/2016/06/loadashNuget.png)

![](/content/images/2016/06/loadashDTNuget.png)


In the `Scripts\lodash\` directory keep only the lodash.d.ts file. Delete the other.

#### Add the DefinitelyTyped React libraries

Create the react folder: `Scripts/typings/react`

*The react DefinitelyTyped should be available from NuGet but there is a naming conflict for React*

*[Description of Issue here](https://github.com/DefinitelyTyped/NugetAutomation/issues/14)*

Therefore, go to [DefinitelyTyped git hub page](https://github.com/DefinitelyTyped/DefinitelyTyped) and downloaded the latest repo.

This repo contains a ton of stuff that you do not need for this blog. Extract the repo. 

Right click on the `Scripts/typings/react` folder and select `Add Existing Item` Browse to the extracted `react` folder select the following as shown in the image then Add.

![](/content/images/2016/04/2016-04-09_1541.png)

I am not sure why the 0.13.3 files are in the master but if you do not remove these you will get duplicate type errors.  Readme.md can be included if you desire.

## Implement the application component
Update the `FlashCardApplicationComponent.tsx`file as follows:

```
class FlashCardApplicationComponent extends React.Component<{}, {}> {
    render() {
        return (
            <div>
                FlashCardApplicationComponent
            </div>
        );
    }
}

ReactDOM.render(
    <FlashCardApplicationComponent />,
    document.getElementById('content')
);
```

#### Update the Feature to use the Component

Edit the `FlashCardApplicationPage.cshtml` file as follows:

```
class FlashCardApplicationComponent extends React.Component<{}, {}> {
    render() {
        return (
            <div>
                FlashCardApplicationComponent
            </div>
        );
    }
}

ReactDOM.render(
    <FlashCardApplicationComponent />,
    document.getElementById('content')
);
```
In order to be able to serve js files from the Features folder, we need to update the `web.config` file in the `Features` folder.  See [Allow serving of js, ts, css and map files from Views folder.](http://www.thefreezeteam.com/2016/04/28/allow-serving-of-js-css-files-from-views-folder/) for details.

After updating the `web.config` file the application should run and the URL should display: "FlashCardApplicationComponent" instead of "Hello World"

# Add StructureMap.MVC5

StructureMap is used as the Ioc container in this template.
[StructureMap.MVC5](https://www.nuget.org/packages/StructureMap.MVC5/3.1.1.134)

![](/content/images/2016/06/StructureMapMVC5Nuget.png)

This adds 4 dependent packages and also adds the files displayed below.

![](/content/images/2016/06/StructureMapMVC5Additions.png)

Now run Update-Package to update to the latest version of the dependent packages.

With StructureMap version 4.0 some changes are needed.

Edit `ControllerConvention.cs` as follows:
```
namespace Tft.FlashCards.DependencyResolution
{
  using System.Web.Mvc;
  using StructureMap.Graph;
  using StructureMap.Pipeline;
  using StructureMap.TypeRules;
  using StructureMap;
  using StructureMap.Graph.Scanning;

  public class ControllerConvention : IRegistrationConvention
  {
    public void ScanTypes(TypeSet types, Registry registry)
    {
      foreach (var type in types.AllTypes())
      {
        if (type.CanBeCastTo<Controller>() && !type.IsAbstract)
        {
          registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
        }
      }
    }
  }
}
```

Edit `DefaultRegistry.cs`

```
namespace Tft.FlashCards.DependencyResolution
{
  using StructureMap;
  using StructureMap.Graph;

  public class DefaultRegistry : Registry
  {
    #region Constructors and Destructors

    public DefaultRegistry()
    {
      Scan(
          scan =>
          {
            scan.TheCallingAssembly();
            scan.WithDefaultConventions();
            scan.With(new ControllerConvention());
          });
      //For<IExample>().Use<Example>();
    }

    #endregion
  }
}
```

Build and run now will give the following error:

`An instance of IControllerFactory was found in the resolver as well as a custom registered provider in ControllerBuilder.GetControllerFactory. Please set only one or the other.`

Now that we have a container we no longer need the explicit setting of the controller factory in Global.asax.cs file

So remove the lines

```
     //Replace Default Controller factor with Feature version
     ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
```
The app should now function again.

Note that a nuget called WebActivatorEx was a dependency and was also installed. "WebActivator is a NuGet package that allows other packages to easily bring in Startup and Shutdown code into a web application. This gives a much cleaner solution than having to modify global.asax with the startup logic from many packages"

Thus the StructuremapMVC.cs file has a Start method that will execute when the site starts up.

# Add Entity Framework 6

Create a root folder named Entities and select it 
![](/content/images/2016/06/EntitiesFolder.png)

Press Ctrl-Shift-A To add a new item.

Select Visual C#->Data-> ADO.NET Entity Data Model And Name it "FlashCardDbContext"

![](/content/images/2016/06/AddDataModel.png)

This will add the Entity Framework NuGet package to your project and Create a FlashCardDbContext.cs file and update your main Web.config.

Make sure the Entity Framework NuGet is up to date using Nuget Package Manager.

# Create Database Initializer

Add a new class named `ModelInitializer` to the `Entities` Folder as follows:

```
namespace Tft.FlashCards.Entities
{
	using System.Collections.Generic;
	using System.Data.Entity;

	public class ModelInitializer: DropCreateDatabaseIfModelChanges<FlashCardDbContext>
	{
		protected override void Seed(FlashCardDbContext aFlashCardDbContext)
		{
			base.Seed(aFlashCardDbContext);
			// initialize Entites here and add them to the context
			//var someEntityTypes = new List<SomeEntityType>
			//{
			//	new SomeEntityType { },
			//	new SomeEntityType { }
			//}

			//someEntityTypes.ForEach(aSomeEntityType => aFlashCardDbContext.SomeEntityTypes.Add(aSomeEntityType));

			aFlashCardDbContext.SaveChanges();
		}
	}
}
```
Tell Entity Framework to use your initializer class by adding the following to the `Global.asax.cs` file as shown and the proper using statments:

```
  // Set Database Initializer
  Database.SetInitializer<FlashCardDbContext>(new ModelInitializer());
```
The intializer will execute on first use. If you want the initializer to execute immediately add the following code in the Global.asax.cs following the above.

```
using (var flashCardDbContext = new FlashCardDbContext())
{
  flashCardDbContext.Database.Initialize(force: false);
}
```
`Global.asax.cs` should now be as follows:

```
namespace Tft.FlashCards
{
  using Entities;
  using Infrastructure;
  using System.Data.Entity;
  using System.Web.Mvc;
  using System.Web.Routing;

  public class MvcApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      // Replace Default ViewEngine with Feature version
      ViewEngines.Engines.Clear();
      ViewEngines.Engines.Add(new FeatureRazorViewEngine());

      //Replace Default Controller factor with Feature version
      ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());

      // Set Database Initializer
      Database.SetInitializer<FlashCardDbContext>(new ModelInitializer());

      using (var flashCardDbContext = new FlashCardDbContext())
      {
        flashCardDbContext.Database.Initialize(force: false);
      }
    }
  }
}

```

# Create Application Entity

Add new class in the `Entities` folder named `FlashCardApplication`

```

```


#Add Nuget packages

