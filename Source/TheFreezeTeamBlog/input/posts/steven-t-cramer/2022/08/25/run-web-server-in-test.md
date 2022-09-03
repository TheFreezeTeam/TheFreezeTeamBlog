---
  Title: Run your full web server inside integration tests
  Published: 08/26/2022
  Tags:
    - CSharp
    - dotnet
    - Kestrel
    - test
  Image: ai-train.png
  Description: "Launch your dotnet web app from your integration tests"
  Excerpt: "How to launch your fully functional dotnet web application from your integration test."
  Author: Steven T. Cramer
---

How to launch your fully functional dotnet web application from your integration test.

## TL;DR

Set `ApplicationName` in the `WebApplicationOptions` sent to `WebApplication.CreateBuilder`

```cs
WebApplication.CreateBuilder
(
  new WebApplicationOptions
  {
    ApplicationName = typeof(Web_Server_Assembly).Assembly.GetName().Name // <==
  }
);
```

## The Story

I have a dotnet Blazor project (`Web.Server.csproj`) that runs fine when launched from VS or `dotnet run`, against which I want to run integration test.

When attempting to launch my Blazor`Web.Server` from my integration tests.  I received the following:

```console
System.InvalidOperationException: Cannot find the fallback endpoint specified by route values: { page: /_Host, area:  }.
...
```

My first thought was. "I must be creating the `WebApplication` differently in my test than I am when running directly."

The code used to create the `WebApplicationBuilder` in `Web.Server.Program` is as follows

```cs
public static Task<int> Main(string[] args)
  {
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    ...
  }
```

The code I was using in the test was:

```cs
WebApplicationBuilder builder = WebApplication.CreateBuilder();
```

I was passing no `args` when running directly so the code was effectively the same.

I deduced that the problem was caused from the context in which I was running.  I mean, the code is the same, so what else could it be?  But what was different about the context?

### One Of These Things Is Not Like The Other

I set a breakpoint after creating the builder, in both the application and the test case.  And went about using the inspector to see if I could tell the difference between them. And yes there was something different.

Inside `builder.WebHost._environment.WebRootFileProvider` I noticed the working one was a `CompositeFileProvider` that contained 2 `FileProviders`

![inspector view of working app ](20220825141419.png)  

 and when running from the test it was `NullFileProvider`

![inspector view of non-working test ](20220825141751.png)  

### Found the difference but why?

Given my code was the same, to determine why dotnet was creating a different `WebRootFileProvider`, I needed to [debug into the dotnet code]() to see where it created this `WebRootFileProvider`.

I probably followed the trace 100x (seriously this whole process took me 2 days) until finally the culprit was found.

```console
 	Microsoft.AspNetCore.Hosting.dll!Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader.ResolveRelativeToAssembly(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment) Line 70	C#
 	Microsoft.AspNetCore.Hosting.dll!Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader.ResolveManifest(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment, Microsoft.Extensions.Configuration.IConfiguration configuration) Line 51	C#
 	Microsoft.AspNetCore.Hosting.dll!Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader.UseStaticWebAssets(Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment, Microsoft.Extensions.Configuration.IConfiguration configuration) Line 27	C#
 	Microsoft.AspNetCore.dll!Microsoft.AspNetCore.WebHost.ConfigureWebDefaults.AnonymousMethod__9_0(Microsoft.AspNetCore.Hosting.WebHostBuilderContext ctx, Microsoft.Extensions.Configuration.IConfigurationBuilder cb) Line 223	C#
 	Microsoft.AspNetCore.Hosting.dll!Microsoft.AspNetCore.Hosting.GenericWebHostBuilder.ConfigureAppConfiguration.AnonymousMethod__0(Microsoft.Extensions.Hosting.HostBuilderContext context, Microsoft.Extensions.Configuration.IConfigurationBuilder builder) Line 182	C#
 	Microsoft.AspNetCore.dll!Microsoft.AspNetCore.Hosting.BootstrapHostBuilder.RunDefaultCallbacks(Microsoft.Extensions.Configuration.ConfigurationManager configuration, Microsoft.Extensions.Hosting.HostBuilder innerBuilder) Line 133	C#
 	Microsoft.AspNetCore.dll!Microsoft.AspNetCore.Builder.WebApplicationBuilder.WebApplicationBuilder(Microsoft.AspNetCore.Builder.WebApplicationOptions options, System.Action<Microsoft.Extensions.Hosting.IHostBuilder> configureDefaults) Line 87	C#
 	Microsoft.AspNetCore.dll!Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(Microsoft.AspNetCore.Builder.WebApplicationOptions options) Line 115	C#
	Testing.Common.dll!TimeWarp.Architecture.Testing.WebApplicationHost<TimeWarp.Architecture.Web.Server.Program>.WebApplicationHost(string[] aUrls, Microsoft.AspNetCore.Builder.WebApplicationOptions aWebApplicationOptions, System.Action<Microsoft.Extensions.DependencyInjection.IServiceCollection> aConfigureServicesDelegate) Line 46	C#

```

## The Problem

When running from a test, the running assembly is your test assembly (`Web.Server.Integration.Tests`) not the web application assembly (`Web.Server`).

"So what?" you might ask.  "Why would that matter?" I didn't think it would, obviously. But yet, it does!

When dotnet builds my `Web.Server` application it creates a json file named `Web.Server.staticwebassets.runtime.json`

This file contains information as to where the csproj file is located and where your web application content should be found. Dotnet tries to resolve the manifest `ResolveManifest`, relative to an assembly named `environment.ApplicationName`.

See the dotnet code snippet from [Microsoft.AspNetCore.Hosting.StaticWebAssets.StaticWebAssetsLoader class](https://github.com/dotnet/aspnetcore/blob/0eaabe0fe5d714753f58ba84c9880403977a7f82/src/Hosting/Hosting/src/StaticWebAssets/StaticWebAssetsLoader.cs#L66-L75):

```cs
private static string ResolveRelativeToAssembly(IWebHostEnvironment environment)
{
    var assembly = Assembly.Load(environment.ApplicationName);
    var basePath = string.IsNullOrEmpty(assembly.Location) ? AppContext.BaseDirectory : Path.GetDirectoryName(assembly.Location);
    return Path.Combine(basePath!, $"{environment.ApplicationName}.staticwebassets.runtime.json");
}
```

`environment.ApplicationName` defaults to the running assembly. And there is no `Web.Server.Integration.Tests.staticwebassets.runtime.json` Thus the dotnet code returns null for the `ResolveManifest` when running from the test.

Snippet of the [ResolveManifest source](https://github.com/dotnet/aspnetcore/blob/0eaabe0fe5d714753f58ba84c9880403977a7f82/src/Hosting/Hosting/src/StaticWebAssets/StaticWebAssetsLoader.cs#L45-L64).

```cs
internal static Stream? ResolveManifest(IWebHostEnvironment environment, IConfiguration configuration)
{
    try
    {
        var candidate = configuration.GetValue<string>(WebHostDefaults.StaticWebAssetsKey) ?? ResolveRelativeToAssembly(environment);
        if (candidate != null && File.Exists(candidate))
        {
            return File.OpenRead(candidate);
        }

        // A missing manifest might simply mean that the feature is not enabled, so we simply
        // return early. Misconfigurations will be uncommon given that the entire process is automated
        // at build time.
        return default;
    }
    catch
    {
        return default;
    }
}
```        

The comment indicates that this is unexpected scenario, and I wonder if throwing an exception here would make sense, but that isn't my call.

### Where is `environment.ApplicationName` configured?

After finally finding what "context" was different, I needed to figure out to make them the same.  Where is this `environment.ApplicationName` configured?

It turns out to be very simple to set. `WebApplication.CreateBuilder` has an overload that takes a `WebApplicationOptions` parameter, and inside that class you can configure the `ApplicationName`. Once I set that to `Web.Server`, everything worked.

```cs
WebApplication.CreateBuilder
(
  new WebApplicationOptions
  {
    ApplicationName = typeof(Web_Server_Assembly).Assembly.GetName().Name // <==
  }
);
```

## Parting words

Don't feel bad if you didn't see this solution, you are not alone. See the references for others that struggled with us.  But now we know. With this blog and search engines hopefully we can find the solution again next time we need it.

## References
Video overview of this post: https://youtu.be/4jMzGw45Kw8
https://stackoverflow.com/questions/72928110/why-wont-unit-tests-connect-to-a-websocket
https://github.com/dotnet/aspnetcore/issues/42657
