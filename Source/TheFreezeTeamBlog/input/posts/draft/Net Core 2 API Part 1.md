Title: Building .Net Core 2 API Part 1 Initial Project and Logging
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

Tools:  

* Visual Studio 2017
* Add [CodeMaid](http://www.codemaid.net/) to visual studio and save the configuration settings in the repo.  This really helps keep the code organized in a consistent manner.
* [Seq](https://getseq.net/)

Planning:
Name of the project:  api.herc.one

Create directory structure for solution.  Derived from various GitHub repositories. If folders are not needed they can be removed.

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

![](/content/images/2018/04/2018-04-01_1646.png)

Choose .net core 2 templates then select the Web Api template. (No Authentication)

![](/content/images/2018/04/2018-04-01_1647.png)

Save the solution file above the source directory and edit it to point to the proper location for the new project show below.
```
├───assets
├───build
├───docs
├───lib
├───samples
├───source
│   └───api.herc.one
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

Set Local Urls [Optional]:

To closely reflect the urls used in various environments, lets create a hosts entry for running local. Edit (requires Admin) "C:\Windows\System32\drivers\etc\hosts" file to add:

`127.0.0.1 local-api.herc.one`

Open launchSettings.json
![](/content/images/2018/04/2018-04-01_1730.png)

I highly prefer to debug under kestrel directly.  So I remove the IIS Settings to simplify.

Change the port for debug mode.  By default a random port is chosen in the high range I prefer to stay close to the default .net core kestrel port (5000).

Lets choose port 5001 as I know I use 5000 for identity server if needed.

Set your `ASPNETCORE_ENVIRONMENT` to `Local`

When complete my `launchSettings.json`file is:

```json
//launchSettings.json
{
  "profiles": {
    "api.herc.one": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "api/values",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Local"
      },
      "applicationUrl": "http://local-api.herc.one:5001/"
    }
  }
}
```

Run the application from Visual Studio and confirm you get the values from the default controller.

Check for any updated Nuget Packages.

### Add Serilog Nuget Packages.
* Serilog.AspNetCore
* Serilog.Sinks.Console
* Serilog.Enrichers.Context
* Serilog.Settings.Configuration
* Collector.Serilog.Enrichers.Assembly
* Serilog.Sinks.Seq
* Destructurama.JsonNet

### Configure Serilog so we can get logging ASAP.

First since we are using serilog we don't need the Logging section in `appSettings.json`, so go ahead and remove that, which will make you appsettings empty for now.

Update `Program.cs` To Initialize the Logger.

#### Program.cs
```
namespace api.herc.one
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
      const string appName = "api.herc.one";
      const string seqUrl = "http://127.0.0.1:5341";
      const string MicrosoftLogSource = "Microsoft";

      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override(MicrosoftLogSource, LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Seq(seqUrl)
        .WriteTo.Console()
        .CreateLogger();

      try
      {
        Log.Information($"Starting {appName}");
        BuildWebHost(args).Run();
        return 0;
      }
      catch (Exception exception)
      {
        Log.Fatal(exception, $"{appName} Host terminated unexpectedly");
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
At this point run the app and verify that you see Console and Seq messages.

- _Note that we have SEQ set to locahost(127.0.0.1`) 
This assumes that SEQ is deployed on the same server,  This is best for local debugging and the config in Program.cs will be overridden in the `Startup.cs` by a config from Appsettings file.  This is just to start logging ASAP and help the devs.  If there is no SEQ on localhost this will not cause a failure just loss of mesages_

Browse to your local [SEQ](http://localhost:5341/) Execute the application.  Refresh SEQ and you should see verbose logging (Can turn down later if needed).

Serilog SEQ
![](/content/images/2018/04/2018-04-01_2132.png)

So we have default logging as early as possible to both console and seq.  But we will likely want more control of the logging via appsettings.json in various environmnets.  So lets configure the log settings for everything other than `Program.cs`.

In Startup.cs add a method called `ConfigureSerilog` as follows:

```
private void ConfigureSerilog()
    {
      LoggerConfiguration LoggerConfiguration = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .Destructure.JsonNetTypes()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironment("ASPNETCORE_ENVIRONMENT")
        .Enrich.With<SourceSystemEnricher<Startup>>()
        .Enrich.With<SourceSystemInformationalVersionEnricher<Startup>>();
      Log.Logger = LoggerConfiguration.CreateLogger();
      ILogger log = Log.ForContext<Startup>();
      log.Information("{SiteName} Site Starting", GetType().Namespace);
    }
```

And call it from your Startup Constructor
```
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
      ConfigureSerilog();
    }
```

Part 2 - Add Testing.
