Title: Free your business logic from your application type
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

When implementing a feature one should **not** create a dependency to an architecture type.  For example, implementing the command handler `CancelPurchaseOrderCommandHandler` should not be dependent on a HttpContext. 

The business logic should be the same for all application types:

- Mobile Application
- Rich Client Application
- Rich Internet Application
- Service Application
- Web Application
- Console Application

Even though your application may start out as a web application, likely you will need to create a windows service, command-line utility or a mobile app to expose some functionality.  The business logic should be independent of the presentation layer, and if it is independent, providing your clients with these new experiences should be much less trouble.

In this blog post, we are going to specifically discuss a service to get the current username.

##IIdentityService

Knowing that to get an Identity for the current user is accomplished differently in the above app types we will build to an abstraction `IIdentityService`.

```csharp
public interface IIdentityService
{
    string Username { get; }
}
```
Although this makes testing much easier, that is NOT the primary reason for it.
The primary reasons are the SID in SOLID.

- [Dependency inversion principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle):  one should �Depend upon Abstractions. Do not depend upon concretions.�
- [Single responsibility principle](https://en.wikipedia.org/wiki/Single_responsibility_principle): a class should have only a single responsibility
- [Interface segregation principle](https://en.wikipedia.org/wiki/Interface_segregation_principle): �many client-specific interfaces are better than one general-purpose interface�

### WebIdentityService 

The specific implementation of the interface for a web application is done in the web app (Notice dependency on `HttpRequest`)

```csharp
namespace Twe.Web.Service
{
  using System.Security.Claims;
  using Microsoft.AspNetCore.Http;

  public class WebIdentityService : IIdentityService
  {
    public WebIdentityService(IHttpContextAccessor aHttpContextAccessor)
    {
      HttpContext = aHttpContextAccessor.HttpContext;
    }

    public string Username => HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

    private HttpContext HttpContext { get; }
  }
}
```


### ConsoleIdentityService
The specific implementation of the interface for a console application is done in the console app (Notice **no** dependency on `HttpRequest`)

```csharp
namespace Twe.Console.Service
{
	public class ConsoleIdentityService : IIdentityService
	{
		public string Username
		{
			get
			{
				return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			}
		}
	}
}
```

The registration of which implementation to use then is done in the app startup:
 
```csharp
    container.Configure(aConfigurationExpression => aConfigurationExpression.AddRegistry<DefaultRegistry>());
```

Where the StructureMap `DefaultRegistry` for the web app is:
 
public class DefaultRegistry : Registry
{
 
    public DefaultRegistry()
    {
        For<HttpContext>().Use(aContex => HttpContext.Current);
        For<HttpRequest>().Use(aContex => HttpContext.Current.Request);
        For<IIdentityService>().Use<WebIdentityService>();
    }
 
}
 
 
And in a Console app
 
```csharp
namespace DuComb.Console
{
	using Twe.Console.Service;
	using StructureMap.Configuration.DSL;

	class ConsoleRegistry:Registry
	{
		public ConsoleRegistry()
		{
			ForSingletonOf<IIdentityService>().Use<ConsoleIdentityService>();
		}
	}
}
```

Now any business logic that needs the username of current user can simply take a dependency on `IIdentityService`.
