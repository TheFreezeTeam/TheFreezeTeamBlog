Title: Blazor Dynamic Dual Mode!
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Image: DynamicDualMode.png

---

## 


The new Blazor 0.8.0 came out [2019-02-05](https://blogs.msdn.microsoft.com/webdev/2019/02/05/blazor-0-8-0-experimental-release-now-available/) and I have been busy updating the [timewarp-blazor](https://timewarpengineering.github.io/blazor-state/docs/Template/TemplateOverview.html) template to support it.

## Server side vs client side

To be clear Blazor can execute on either, server side 
or client side.  The Component doesn't have to change.

When a Blazor application starts, it runs 1 of 2 JavaScript files that determine where the browser is going to execute the application.

The server side execution sends DOM updates to the browser via SignalR.

![server side](https://docs.microsoft.com/en-us/aspnet/core/razor-components/index/_static/aspnet-core-razor-components.png)

Where as the client side executes the application inside the browser using WebAssembly.

![client side](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/blazor/index/_static/blazor.png)

There are [advantages and disadvantages](https://docs.microsoft.com/en-us/aspnet/core/razor-components/hosting-models?view=aspnetcore-3.0) of each hosting model.

One big advantage of server side is the speed at which it renders the first page.  The JavaScript loaded for server side is only 57KB. Another is the ability to debug with Visual Studio.

One of the downsides of server side is reduced scalability.  The server has to manage all the work that the normal server would plus the work of the browser for each connection.

A big client side advantage is offloading much of the work from the server to the browser, thus your site will scale better. Yet to do this one has to download more code from the server and thus it takes longer and you have slower first page loads.

## Dynamic Dual Mode

*Wouldn't it be nice to have both fast first page load and allow for more scalability by running on the client?*

With the timewarp-blazor template we load the site on the first visit to run from the server so you have fast load times and quick interactivity.  Yet we also load/cache the client side version in parallel without blocking the main window.

Upon refreshing of the screen or on subsequent visits to the site we transition to client-side execution. Thus offloading the burden from the server and improving scalability.

This is accomplished by using a flag stored in `localStorage`. On the first visit to the site this flag won't exist and thus we use server-side execution and launch the loading of the client side version which adds the flag.

Upon next visit the `localStorage` value will exist and we launch client side execution which should be cached.

## Specify execution side

You may want the ability to easily toggle the execution side. This allows for testing of both sides, and currently the debugging experience on server side is much better.
For this ability we added another `localStorage` value `ExecutionSide` that allows you to specify where to run the Blazor application. This can be changed in your browser devtools to client/server and then reload the page.

To see this in action, our base timewarp-blazor template is running on azure 
[here](https://blazor-state.azurewebsites.net/)

To get started with Dynamic Dual Mode in your own app install the templates

```
dotnet new --install TimeWarp.AspNetCore.Blazor.Templates
```

and create and run the new application

```
dotnet new timewarp-blazor -n MyBlazorApp
dotnet run
```

Happy Dualing :)

Acknowledgments:
I believe I first heard about switching between client and server side execution from [Robin Sue Suchiman](https://github.com/Suchiman) on the Blazor Gitter Channel(https://gitter.im/aspnet/Blazor)  Come join us.

Reference:
[Razor Components server side](https://docs.microsoft.com/en-us/aspnet/core/razor-components/?view=aspnetcore-3.0) and 
[Razor Components client side](https://docs.microsoft.com/en-us/aspnet/core/client-side/spa/blazor/?view=aspnetcore-3.0)

Tags: dotnetCore Blazor C#
Author: Steven T. Cramer
Date: 2019-02-20
Updated: 2019-03-31 (Change names back from Razor Components to Blazor)

