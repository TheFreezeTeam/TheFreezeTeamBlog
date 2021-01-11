Title: NodaTime .NET Core 2  json serialization configuration
Tags: 
  - NodaTime 
  - NetCore 
  - C# 
Author: Steven T. Cramer
Image: NodaTime.png

---
[NodaTime ](https://nodatime.org)is a good replacement library for the .net DateTime.

add the nuget pacakge NodaTime.Serialization.JsonNet

To configure NodaTime for json seralization in .net core add the following to your serviceCollection:

```csharp
  public void ConfigureServices(IServiceCollection aServiceCollection)
    {
      aServiceCollection.AddMvcCore()
        .AddJsonFormatters(jsonSerializerSettings => jsonSerializerSettings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
    }
```

Tools:
Visual Studio 2017
.net Core 2
NodaTime 2.2.3
NodaTime.Serialization.JsonNet 2.0.0

