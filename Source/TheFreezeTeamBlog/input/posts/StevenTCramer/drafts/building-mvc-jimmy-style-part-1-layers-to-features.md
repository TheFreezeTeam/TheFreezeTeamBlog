Title: Building MVC 5 Jimmy Style Overview
Tags: 
  - Jbogard
  - Mvc
  - C#
  - Entity-framework-5
  - Entity-framework
  - Mediatr
  - Automapper
Author: Steven T. Cramer
Excerpt: RBuilding MVC 5 Jimmy Style Overview
Description: Building MVC 5 Jimmy Style Overview
Published: 03/12/2099
---

tags: [jbogard, mvc, features, c#, entity-framework-5, entity-framework, mediatr, automapper]

This multi-part blog is a step by step tutorial that transforms a default MVC 5 application into [Jimmy Bogard's](https://lostechies.com/jimmybogard/) Feature-oriented configuration as described in his [presentation](https://lostechies.com/jimmybogard/2015/07/02/ndc-talk-on-solid-in-slices-not-layers-video-online/). His sample project located [here](https://github.com/jbogard/ContosoUniversity "here") was used to guide the way.  The VSIX package and source code of this presentation are available [here](/assets/pictures/jimmy-mvc/JimmyMvcVsixProject.vsix).


* Background
* Create Default MVC 5 Application
* Rename Views to Features
* Removing the Controllers Folder
* The Business Case

# Background #

**From:**
![](/content/images/2016/01/Layers.png)

**To:** 
![](/content/images/2016/01/Features.png)
![NewProject](/assets/pictures/jimmy-mvc/Features.png)



## What is a feature ##

A feature is a small vertical slice of a web site. The **M**odel, **V**iew and **C**ontroller of a **single url**.  It can contain the following:
- Query
- QueryValidater
- QueryHandler
- Result
- Command
- CommandValidater
- CommandHandler
- UiController
- MappingProfile
- View  

A Feature handles the `get` and or `post` of an http requests for a single url.  
Examples: `http://localhost/User/Index`, `http://localhost/User/Create` 
It can be synchronous or asynchronous, and can have a redirect to url/Feature upon command success.  Features use the **CQRS light** design pattern.

## CQRS Light##
The CQRS (Command Query Responsibility Segregation) pattern means many different thing to different people.  The full blown CQRS with dual domain models and event sourcing etc. gets a bit overwhelming rather quickly.  "[CQRS Light](https://lostechies.com/jimmybogard/2015/05/05/cqrs-with-mediatr-and-automapper/)" is just the splitting of Features into Queries and Commands.  A single domain model is the starting point and will not be split unless circumstances drive the need, which is unlikely.  There is no event sourcing and most messaging is done in process using MediatR.

The Http protocol correlates rather well with CQRS Light.

Get = Read = Query
Post = Write = Command

The MediatR Nuget package will be used to implement CQRS light.

![MediatR](/assets/pictures/jimmy-mvc/MediatR.png)

This should all become clear as we implement. Let's get started.



