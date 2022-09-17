Title: Building .Net Core 2 API Part 2 Add Test Project
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

### Create Test Project

Although I am not a TDD zealot I do think testing should be treated as a first class citizen. So if you create the tests first or after I don't care as long as we actually test the app in an automated way.

For testing we are going to use Patrick Lioi's [fixie](https://github.com/fixie/fixie)

Create new Test Project named api.herc.one.tests

![](/content/images/2018/04/2018-04-01_2159.png)
Add the following nuget packages:
* Fixie 2.0 or greater (this is done with 2.0.0-beta-0001),
* Shouldly,
* Microsoft.AspNetCore.TestHost

Add Reference to the package under test `api.herc.one`.

Edit the csproj file to add the cli tools. as follows:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFrameworks>netcoreapp2.0;net471</TargetFrameworks>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Fixie" Version="2.0.0-beta-0001" />
    <PackageReference Include="Microsoft.AspNetCore.TestHost" Version="2.0.2" />
    <PackageReference Include="Shouldly" Version="3.0.0" />
    <DotNetCliToolReference Include="Fixie.Console" Version="2.0.0-beta-0001" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\api.herc.one\api.herc.one.csproj" />
  </ItemGroup>

</Project>
```

TDD says you should do a test before you implement the feature.  Before you do a test you do a "User Story" or create a "Product Backlog Item" (PBI)

But remember we are iterating.  So this feature can be improved in a later sprint.

# PBI 1: Endpoint 1 Health
As a consumer of the API I want to know the overall health status of the system.

Architecture:
Route: `/api/health` Get
Return Status: 200
Return Body: json (TBD)

Acceptance Criteria.
We don't know exactly what the health response will be at this point but for this PBI the requirement is that we return a 200 Status if all systems are functional. And non 200 if non functional.

# Endpoint 1 Test Case.
Do a get request to  /api/health and confirm 200 status is returned.

Given this is the only feature we have and no other dependencies we are not going to add a negative health check test yet.

## Tests organization
We want to organize our tests to mirror the organization of the source under test.

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
│   └───api.herc.one.tests
│       ├───Features
│       │   └───Health
└───tools
```

Inside this folder create a new file named HealthTests.cs

```
namespace api.herc.one.tests
{
  using api.herc.one.tests.Configuration;
  using Microsoft.AspNetCore.TestHost;
  using System;
  using System.Net.Http;
  using System.Threading.Tasks;

  public class HealthTests
  {
    public HealthTests()
    {
      Console.WriteLine("HealthTests");
      TestServer = new ApiTestServer();
      HttpClient = TestServer.CreateClient();
    }

    protected HttpClient HttpClient { get; }
    protected TestServer TestServer { get; }

    public async Task ShouldReturnJson()
    {
      HttpResponseMessage httpResponse = await HttpClient.GetAsync("/api/health");
      httpResponse.EnsureSuccessStatusCode();
    }
  }
}
```

This requires the ApiTestServer which is just a easy way to launch the system under test.

\Herc\tests\api.herc.one.tests\Configuration\ApiTestServer.cs
```
namespace api.herc.one.tests.Configuration
{
  using Microsoft.AspNetCore;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.TestHost;
  using System.Net;

  public class ApiTestServer : TestServer
  {
    public ApiTestServer() : base(WebHostBuilder()) { }

    private static IWebHostBuilder WebHostBuilder() =>
      WebHost.CreateDefaultBuilder()
        .UseStartup<Startup>()
        .UseEnvironment("Local")
        .UseKestrel(options =>
        {
          options.Listen(IPAddress.Any, 5001);
        });
  }
}
```
Run test and confirm you get 404 error: as we have not yet implemented the feature.

Next Implementing Health Check Feature.


