Title: Automatically Embed resources in dotnet
Published: 26/11/2021
Image: 2021-11-26-07-21-51.png
Tags: 
  - C# 
  - DirectoryBuildProps
  - EmbeddedResource
Author: Steven T. Cramer
Description: How to automatically embeded resources in your dotnet dlls?
Excerpt: Inside your `Directory.Build.props` file add the following
---

# Automatically Embed Resoures in dotnet

## Scenario

I am working on source generators and I want to include my `scriban` template in the dll as an embedded resource. This makes for easier to read templates than using multiline strings in the C# code itself. 

## Solution

Inside your `Directory.Build.props` file add the following:

```csproj
  <Target Name="EmbedLocal" BeforeTargets="PrepareForBuild">    
    <!-- Include each file with given extension from None collection to EmbeddedResource-->
    <ItemGroup>
      <EmbeddedResource Include="@(None -> WithMetadataValue('Extension', '.scriban'))" />
      <EmbeddedResource Include="@(None -> WithMetadataValue('Extension', '.MyCoolExtension'))" />
    </ItemGroup>
  </Target>
```

## Validation

To validate your resources are included you can use [Telerik's JustDecomplie](http://www.telerik.com/products/decompiling.aspx)


![](../images/2021-11-26-07-21-51.png)

# References

https://dominikjeske.github.io/source-generators/
https://stackoverflow.com/questions/51607558/msbuild-create-embeddedresource-before-build
https://docs.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2022
https://stackoverflow.com/questions/6631059/is-there-a-way-to-see-the-resources-that-are-in-a-net-dll
