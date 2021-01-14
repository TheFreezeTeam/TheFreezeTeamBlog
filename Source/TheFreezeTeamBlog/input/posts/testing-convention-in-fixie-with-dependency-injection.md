Title: A Testing Convention for Fixie with Dependency Injection support
Published: 01/29/2019
Tags: 
  - Fixie 
  - Testing 
  - dotnetcore 
  - C#
Author: Steven T. Cramer
Image: TestingConvention.png
Excerpt: A dotnet core Testing Convention for Fixie that supports Dependency Injection
Description: A dotnet core Testing Convention for Fixie that supports Dependency Injection
---

I utilize [Fixie](http://fixie.github.io/) for my dotnet core unit and integration testing.

> "Fixie is a .NET modern test framework similar to NUnit and xUnit, but with an emphasis on low-ceremony defaults and flexible customization", 

The goals of the testing convention are:
* Support consistent paradigm by using constructor injection.
* Tell Fixie how to find the tests.
* Tell Fixie how to instantiate the tests.
* Tell Fixie how to execute the tests.

## How to find the tests.

This is easy we inherit from the default Discovery class and then add a `Where` delegate.  The delegate allows us to not treat methods named "Setup" as tests.

```csharp
public class TestingConvention : Discovery 
{
      public TestingConvention()
    {
      Methods.Where(aMethodExpression => aMethodExpression.Name != nameof(Setup));
    }
}
```

## Support consistent paradigm by using constructor injection.
Dependency Injection is central to dotnet core.  We configure the DI container in Startup.cs and then write our code with explicit dependencies called out in the constructor and don't think anything of it.

I want to write my test classes the same way.  If a test class has a test dependency I can inject it.  To accomplish this we create a `ServiceCollection`,
configure it with the all the test cases. Then build the `ServiceProvider`.

```csharp
    public TestingConvention()
    {
      var testServices = new ServiceCollection();
      ConfigureTestServices(testServices);
      ServiceProvider serviceProvider = testServices.BuildServiceProvider();
      ServiceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
    }

    private void ConfigureTestServices(ServiceCollection aServiceCollection)
    {
      // Register any dependencies you need here.
      // Scrutor Nuget package gives us the "Scan" method.
      aServiceCollection.Scan(aTypeSourceSelector => aTypeSourceSelector
        .FromAssemblyOf<TestingConvention>()
        .AddClasses(action: (aClasses) => aClasses.TypeName().EndsWith("Tests"))
        .AsSelf()
        .WithScopedLifetime());
    }
```

## How to execute the tests.

To change the default execution in fixie we implement the `Execution` interface.
To utilize the DI container, instead of creating an instance directly we let the service provider create it for us.

To support the ability to have Setup run after construction we use reflection to find the method and execute it.

```csharp
    public void Execute(TestClass aTestClass)
    {
      aTestClass.RunCases(aCase =>
      {
        using (IServiceScope serviceScope = ServiceScopeFactory.CreateScope())
        {
          object instance = serviceScope.ServiceProvider.GetService(aTestClass.Type);
          Setup(instance);

          aCase.Execute(instance);
        }
      });
    }

    private static void Setup(object aInstance)
    {
      System.Reflection.MethodInfo method = aInstance.GetType().GetMethod(nameof(Setup));
      method?.Execute(aInstance);
    }
```

So now fixie can find instantiate and execute our tests using DI.

## The code
[TestingConvention.cs](https://github.com/TimeWarpEngineering/blazor-state/blob/master/test/BlazorState.Server.Integration.Tests/Infrastructure/TestingConvention.cs)

Dependencies:
[Scrutor](https://github.com/khellang/Scrutor)
[fixie](http://fixie.github.io/)


Tags: Fixie,  Testing, dotnetCore, Dependency Injection, C#
Credits: Patrick Lioi @plioi

![Logo](/content/images/2019/01/Logo.svg)

