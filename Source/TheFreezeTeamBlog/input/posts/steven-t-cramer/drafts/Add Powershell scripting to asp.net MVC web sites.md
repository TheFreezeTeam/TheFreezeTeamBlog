Title: Add Powershell scripting to asp.net MVC web sites
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

A User eXperience is a wonderful thing.  The self-discovery afforded by a well-designed interface massively shortens the learning curve for an application user. 

So why would anyone waste time adding scripting ability when I have such a perfect UX?  I am reminded of the old Mac vs Dos argument back in the 90s.  But just like the 90s I still think the Amiga was the best which embraced both the GUI and the CLI (Marketing is very important also but off topic).

A newer version of this argument could be CLI vs IDE. I would argue that the self-discovery of a good UX will surely shorten the learning curve.  So then why do these CLI's remain?  Why is .net core now run from the command line?

I believe the answer is flexibility and power.  Sure the learning curve for the CLI is higher.  But the ability to implement ad-hoc use cases is of great benefit.  

Imagine you have this great application that manages all of your contacts in the most beautiful manner.  And you have this old application that actually has all your contact data.  The new application has the best add new contact screen you have ever seen.  After the 15th contact of the 1297 contacts you have, you begin to realize that even though learning how to add a contact was real easy now you wish you had a way to add them quicker.

I think there are two use cases for most apps.  First, there is the normal user and the learning curve, this is where the UX wins the day. Secondly, there are the power-users who know the UX but now want to automate a use case above and beyond what the UX affords. This is where scripting ability saves days.

An example app that does a great job of building great UX and exposing all the features to the CLI is my favorite DVCS [Plasticscm ](https://www.plasticscm.com/). 

![](2016-11-01_1910.png)

Yet everything you can do from the GUI can be done from the CLI.

So now that we have that out of the way lets make this choice even easier by making the adding of scripting abilities a rather easy thing to do.  This assumes you have built your website following the SOLID ideas described in a previous blog [Building MVC Jimmy Style](http://www.thefreezeteam.com/2015/08/10/building-mvc-jimmy-style/).

Actually, if you just use the [MediatR](https://github.com/jbogard/MediatR) pattern and IOC this should be adaptable.

All of our feature controllers are implemented with MediatR commands similar to the following:

```csharp
      public ActionResult Get(GetQuery aQuery)
      {
        GetResult result = Mediator.Send(aQuery);
        return ....
      }
```
The key is nothing takes place in the controller.  All logic is pushed down into the command handler or the domain objects.  This gives us a nice place to insert PowerShell capabilities.  (Yes you need to consider that PowerShell will have a different pipeline but this is a start)  The goal is to convert all Commands to also be cmdlets. 

# Add Powershell to app
`PM> Install-Package Microsoft.PowerShell.5.ReferenceAssemblies`

[To implement a PowerShell command in C#](https://www.simple-talk.com/dotnet/net-development/using-c-to-create-powershell-cmdlets-the-basics/) we need to inherit from `Cmdlet`. Thus we create a common base class for all our Commands as follows:

```csharp
public class BaseCommand<TResult> : Cmdlet, IRequest<TResult>
  {
    protected IMediator Mediator { get; set; }

    protected override void BeginProcessing()
    {
      base.BeginProcessing();
      //Authentication Check get User etc...
      // Remember this is not in the httprequest pipeline.
      // If the httprequest is a dependcy you may want to consider 
      // if your command handler needs refactoring.
      Mediator = Ioc.Container.GetInstance<IMediator>();
    }

    protected override void ProcessRecord()
    {
      base.ProcessRecord();

      TResult result = Mediator.Send(this);

      WriteObject(result);

    }
  }
}
```

The BeginProcess is used to make sure the Ioc Container is configured and to get the Mediator Instance.  Given the MediatR pattern is "model in model out" this maps great to the PowerShell pipeline.  We simply pass in the model and write out the result.

# Update our Commands

So now if we want to expose a command as a Cmdlet we need to inherit from the BaseCommand and decorate the class with PowerShell attributes.

```csharp
    [Cmdlet(VerbsCommon.Get, "FlashCardApplication")] //Get-FlashCardApplication
    [OutputType(typeof(GetResult))]
    public class GetQuery : BaseCommand<GetResult> { }
```

Rebuild your dll and you now have powershell cmdlets.

To Test they are exposed: 
open PowerShell 
cd to the bin directory of you dll
PS> Import-Module .\<yourdllname>.dll
PS> Get-Module
PS> Get-Command -module <YourModuleaName>

I would like to say you are done and that is all there is to it,  but that isn't quite the case.  When a website loads your dll it has some intelligence and loads your dependencies as well. The web config has "bindingRedirect" tags that tell the loader if the package depends on a particular version of a dll in a range give them this version instead.  Powershell doesn't do this for you. So in the Powershell environment we need to implement the binding redirect manually to load the dependencies.  To accomplish this I use [some code](https://gist.github.com/JamesRandall/444f3365751edb130bef197f2222cfa5) adapted from [James Randall](https://gist.github.com/JamesRandall) 

```
function Load-Dependencies
{
  Write-Host "Configuring Load Dependency Method"
  # Load your target version of the assembly
  #$automapper = [System.Reflection.Assembly]::LoadFrom("C:\Plastic\ReactFlashCard\Source\FlashCards\bin\AutoMapper.dll")
  #Write-Host $automapper
  
  $onAssemblyResolveEventHandler = [System.ResolveEventHandler]	{
    param($sender, $e)
    
    Write-Host $e.Name
    # You can make this condition more or less version specific as suits your requirements
    if ($e.Name.StartsWith("AutoMapper")) 
    {
      return $automapper
    }
    foreach($assembly in [System.AppDomain]::CurrentDomain.GetAssemblies()) 
    {
      if ($assembly.FullName -eq $e.Name) 
      {
        return $assembly
      }
    }
    return $null
  }

  [System.AppDomain]::CurrentDomain.add_AssemblyResolve($onAssemblyResolveEventHandler)
}
```

# Powershell script
# Binding redirect in Powershell

https://www.simple-talk.com/dotnet/net-development/using-c-to-create-powershell-cmdlets-the-basics/


