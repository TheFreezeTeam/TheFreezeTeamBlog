Title: Building .Net Core 2 API Part 3 Implement Health Check Feature
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

## What is a feature ##

A feature is a small vertical slice of a web site. The **M**odel, **V**iew and **C**ontroller of a **single url**.  Given we are implmenting an API we have no View.

A Feature can contain the following:

- Request
- Validator
- Handler
- Controller
- View
- Mapper
  
For our API a Feature handles the requests for a single url & http verb combination.

Add `Features` Folder to the api solution.
Add `Health` Folder under `Features`

```
C:.
├───Controllers
├───Features
│   └───Health
├───Properties
└───wwwroot
```



[Twe.Common](http://TODO add a link)
MediatR
MediatR.Extensions.Microsoft.DependencyInjection
FluentValidation.AspNetCore
NodaTime
NodaTime.Serialization.JsonNet
GlobalExceptionHandler
GlobalExceptionHandler.ContentNegotiation.Mvc


https://jimmybogard.com/domain-command-patterns-validation/

http://josephwoodward.co.uk/2017/12/global-exception-handler-version-2-released
