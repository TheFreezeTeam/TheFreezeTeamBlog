Title: Blazor Hosting Location
Tags: 
  - Blazor
  - C#
Author: Steven T. Cramer
Image: BlazorHostingLocation.png

---
# Client-side or Server-side
Thanks to the AspNetCore Team for changing the name back to Blazor!

Here is a handy class that you can use in Blazor to determine if you are executing client-side or server-side. Its included in [Blazor-State](https://github.com/TimeWarpEngineering/blazor-state) but if you just want it by itself here it is.

```csharp
namespace BlazorState.Services
{
  using System;

  public class BlazorHostingLocation
  {
    public bool IsClientSide => HasMono;
    public bool IsServerSide => !HasMono;
    public bool HasMono => Type.GetType("Mono.Runtime") != null;
  }
}
```

I use it in my demo app to display the execution side in the header. It is even more handy when used in conjunction with [Blazor Dynamic Dual Mode](https://thefreezeteam.com/razor-components-dynamic-dual-mode/) where you can switch back and forth with ease. 

file: `BlazorLocation.razor`

```csharp
@inherits BlazorLocationModel

@if (BlazorHostingLocation.IsClientSide)
{
  <div>ClientSide</div>
}
else
{
  <div>ServerSide</div>
}
```

file: BlazorLocation.razor.cs

```csharp
namespace TestApp.Client.Components
{
  using BlazorState.Services;
  using Microsoft.AspNetCore.Components;

  internal class BlazorLocationModel
  {
    [Inject] public BlazorHostingLocation BlazorHostingLocation { get; set; }
  }
}

```

----

References:
https://stackoverflow.com/questions/721161/how-to-detect-which-net-runtime-is-being-used-ms-vs-mono

Author: Steven T. Cramer
Date: 2019-03-31 14:29:24
Tags: Blazor AspNetCore Mono WebAssembly
