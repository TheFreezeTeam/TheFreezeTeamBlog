---
DocumentName: FindAzureUserById
Title: Find Azure DevOps User by ID
Published: 08/30/2019
Image: michael-dziedzic-1bjsASjhfkE-unsplash.jpg
Tags: 
  - AzureDevOps 
  - AzureArtifacts
Author: Steven T. Cramer
Description: User 0ec552a3-706e-462e-b46c-a488fe2dcba2 lacks permission to complete this action. Which user is that?
Excerpt: User 0ec552a3-706e-462e-b46c-a488fe2dcba2 lacks permission to complete this action. Which user is that?
---


## The scenario

In the `Development` stage of my Azure Release process, I want to deploy a NuGet package to Azure Artifacts.  If all looks good, I can approve the push to production, which deploys it to NuGet.

## The Error

```console
The nuget command failed with exit code(1) and error(System.AggregateException: One or more errors occurred. --->
NuGet.Protocol.Core.Types.FatalProtocolException: Unable to load the service index for source 
https://timewarpenterprises.pkgs.visualstudio.com/_packaging/dfe61826-f0b9-46b3-a391-23626fc363f2/nuget/v3/index.json. --->
System.Net.Http.HttpRequestException: 
Response status code does not indicate success: 403 
(Forbidden - User '8e67c1e7-85bf-4ab6-883c-9e4f9eab747d' lacks permission to complete this action.
You need to have 'ReadPackages'. 
(DevOps Activity ID: 6BC7EC93-FAE4-404D-AEC5-A7A3D737506B)).
```

### The key part of the error:

```console
User '8e67c1e7-85bf-4ab6-883c-9e4f9eab747d' lacks permission to complete this action.
You need to have 'ReadPackages'
```

## The User Experience

The error message does a great job of giving a precise ID of the user.
Yet doesn't give a descriptive name of that user.
Nor could I find anyway to search via the Azure Dev Ops UI to find a user by ID.

## The Workaround

Thankfully Azure Dev Ops has a nice [REST API](https://docs.microsoft.com/en-us/rest/api/azure/devops/graph/users/list?view=azure-devops-rest-5.1) that comes to our rescue.
By no means should this be considered a proper user experience.

Using the user list endpoint:

>`https://vssps.dev.azure.com/{organization}/_apis/graph/users?api-version=5.1-preview.1`

One can query all the users and get back json.
From the result you can search for the GUID that was in the error message and determine the `Name` of that account.
With the name you can then search in Azure Dev Ops for the user and grant them the appropriate permissions.

>**Don't forget a good superhero keeps their code clean and tested.
With actionable error messages.**
