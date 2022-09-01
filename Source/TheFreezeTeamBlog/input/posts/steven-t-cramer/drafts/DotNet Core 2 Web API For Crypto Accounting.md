Title: DotNet Core 2 Web API For Crypto Accounting
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

Tools:  Visual Studio 2017
Add [CodeMaid](http://www.codemaid.net/) to visual studio and save the configuration settings in the repo.  This really helps keep the code organized in a consistent manner.

Planning:
Name of the project:  crypto.timewarp.engineering


Create directory structure for solution.  Derived from various github repos. If folders are not needed they can be removed.

```
Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----         1/6/2018   2:42 PM                assets
d-----         1/6/2018   2:42 PM                build
d-----         1/6/2018   2:42 PM                docs
d-----         1/6/2018   2:42 PM                lib
d-----         1/6/2018   2:42 PM                samples
d-----         1/6/2018   3:08 PM                source
d-----         1/6/2018   2:42 PM                tests
d-----         1/6/2018   2:42 PM                tools
-a----         1/6/2018   1:59 PM           4832 .gitignore
-a----         1/6/2018   1:59 PM            936 README.md

```
Create new solution.

Choose .net core 2 templates then select the Web Api template. (No Authentication)

Move the solution file above the source directory and edit it to point to the proper location for the new project show below.
```
├───assets
├───build
├───docs
├───lib
├───samples
├───source
│   └───crypto.timewarp.engineering
│       ├───bin
│       │   └───Debug
│       │       └───netcoreapp2.0
│       ├───Controllers
│       ├───obj
│       │   └───Debug
│       │       └───netcoreapp2.0
│       ├───Properties
│       └───wwwroot
├───tests
└───tools
```

Execute and validate that you get results from the simple example controller.

```
[
"value1",
"value2"
]
```
### Configure Debug

![](/content/images/2018/02/2018-02-23_1330.png)

Change the port for debug mode.  By default a random port is chosen in the high range I prefer to stay close to the default .net core kestrel port (5000) to simplify.

Lets choose port 5001 as I know I use 5000 for identity server if needed.

Set your `ASPNETCORE_ENVIRONMENT` to `Local`

Optional:

To closely reflect the urls used in various environments, lets create a hosts entry for running local. Edit (requires Admin) "C:\Windows\System32\drivers\etc\hosts" file to add:

`127.0.0.1 local-crypto.timewarp.engineering`

Run the application from Visual Studio again and go to
http://local-crypto.timewarp.engineering:5001/api/values

Update all the nuget packages.

### Add Nuget Packages we are going to use.
[Twe.Common](http://TODO add a link)
MediatR
Loupe.Extensions.Logging
MediatR.Extensions.Microsoft.DependencyInjection 
FluentValidation.AspNetCore
NodaTime
NodaTime.Serialization.JsonNet
Serilog.AspNetCore
Serilog.Sinks.Console
Serilog.Enrichers.Context
Serilog.Settings.Configuration
Collector.Serilog.Enrichers.Assembly
Serilog.Sinks.Seq
Destructurama.JsonNet


### Configure Serilog so we can get logging ASAP.

Update `Program.cs` To Initialize the Logger. Actually we have two loggers Serilog and Loupe as I am trying to compare. I will choose later.

#### Program.cs
```
namespace crypto.timewarp.engineering
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Serilog;
  using Serilog.Events;
  using System;

  public class Program
  {
    public static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseSerilog()
        .Build();

    public static int Main(string[] args)
    {
      Gibraltar.Agent.Log.StartSession();

      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
        .Enrich.FromLogContext()
        .WriteTo.Seq("http://127.0.0.1:5341")
        .WriteTo.Console()
        .CreateLogger();
      try
      {
        Log.Information("Starting service.littlecaesars.com");
        BuildWebHost(args).Run();
        return 0;
      }
      catch (Exception exception)
      {
        Log.Fatal(exception, "Host terminated unexpectedly");
        return 1;
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }
  }
}
```
- _Note that we have SEQ set to locahost(127.0.0.1`) 
This assumes that SEQ is deployed on the same server,  This is best for local debugging and the config in Program.cs will be overridden in the Start by a config from Appsettings file.  This is just to start logging ASAP and help the devs.  If there is no SEQ on localhost this will not cause a failure just loss of mesages_

Load your local [SEQ](http://localhost:5341/) Execute the application.  Refresh SEQ and you should see verbose logging (Can turn down later if needed).

Serilog SEQ
![](/content/images/2018/01/image-1.png)

Loupe Log Viewer
![](/content/images/2018/01/image-9.png)

### Create Test Project

Although I am not a TDD zealot I do think testing should be treated as a first class citizen. So if you create the tests first or after I don't care as long as we actually test the app in an automated way.

For testing we are going to use Patrick Lioi's [fixie](https://github.com/fixie/fixie)

Create new Test Project named crypto.timewarp.engineering.tests

Add the following nuget packages:
Fixie,
Shouldly,

# Endpoint 1 PBI
As a consumer of the API I want to know the overall health status of the system.
Route: `/api/health`

We don't know exactly what the health response will be but the first requirement is that we return a 200 Status if all systems are functional.  Given at this point we have nothing other than this call.  We want to build the First Feature.

# Endpoint 1 Test.

Now we want to write our first test.

In the test project create a Folder named `Features` and inside Features a folder named Health.  

```
C:.
├───assets
├───build
├───docs
├───lib
├───samples
├───source
├───tests
│   └───crypto.timewarp.engineering.tests
│       ├───Features
│       │   └───Health
└───tools
```

Inside this folder create a new file named HealthTests.cs

We are going to write only the Happy Path/ Positive Path for now.

Add Nuget Microsoft.AspNetCore.TestHost





















