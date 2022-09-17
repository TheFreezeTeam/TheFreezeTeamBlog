Title: Dependency Injection Template
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

So in handlers controllers etc... when you need a new dependency you do the same thing over and over.

Add the dependeceny to the constructor params.
Create a private property to hold it.
and in the constructor assign the param to the private property.

Can this be automated via some "Add dependency"

ask for the type then create all from that.
ISomeInterface

![ID](http://xxx)
