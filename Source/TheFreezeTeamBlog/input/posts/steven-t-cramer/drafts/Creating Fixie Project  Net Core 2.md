Title: Creating Fixie Project .Net Core 2
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---


##Step-by-step:

Create new Class Library named crypto.timewarp.engineering.tests 
*File->New->Project*

![](/content/images/2018/01/image-22.png)

Add Nuget packages from Nuget Package Manager Console. 
*Tools->Nuget Package Manager->Nuget Package Manager Console*

As of this writing we are using alpha version of fixie.
and Shouldy is 2.8.3

```
PM> Install-Package Fixie -Version 2.0.0-alpha-0004
...
PM> Install-Package Shouldly
...
```

We need to add a Cli Tool Reference to Fixie.Console currently the only way to do that is to manually edit the project file as below:

#### crypto.timewarp.engineering.tests.csproj

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Fixie" Version="2.0.0-alpha-0004" />
    <PackageReference Include="Shouldly" Version="2.8.3" />
    <DotNetCliToolReference Include="Fixie.Console" Version="2.0.0-alpha-0004" />
  </ItemGroup>
</Project>
```
Rename the default `class.cs` file created in the template to `SettingsTests.cs` and implement code below:

```csharp

namespace Day0Tests
{
  using Day0;
  using Shouldly;
  using System;
  using System.IO;

  public class Day0Tests
  {

    public StringWriter StringWriter { get; } = new StringWriter();

    private void CaptureConsole(string aInput)
    {
      var stringReader = new StringReader(aInput);
      Console.SetIn(stringReader);
      Console.SetOut(StringWriter);
    }

    public void Case1()
    {
      //Arrange
      string input = "Welcome to 30 Days of Code!";
      string expectedOutput = "Hello, World.\r\nWelcome to 30 Days of Code!\r\n";
      CaptureConsole(input);

      //Act
      Program.Main(null);

      //Gather Output
      StringWriter.Flush();
      string output = StringWriter.ToString();

      //Assert
      output.ShouldBe(expectedOutput);
    }

    public void Case2()
    {
      //Arrange
      string input = "HackerRank is the best!";
      string expectedOutput = "Hello, World.\r\nHackerRank is the best!\r\n";
      CaptureConsole(input);

      //Act
      Program.Main(null);

      //Gather Output
      StringWriter.Flush();
      string output = StringWriter.ToString();

      //Assert
      output.ShouldBe(expectedOutput);
    }
  }
}
```

The Code is rather simple and follows the Arrange Act Assert concept.  All the conventions are the Fixie Default.

More fixie blogs to come as I update the JimmyMVC to demonstrate integration tests.

## CaptureConsole
A nice method is `CaptureConsole` to pass input (stdin) and capture output (stdout) from a console application.

