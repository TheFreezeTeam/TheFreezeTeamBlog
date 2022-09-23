Title: Flash Cards with JimmyMVC and React
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

# The Business Case

I am going on vacation to Thailand and want to learn to speak the Thai language. The goal is to make the simplest viable product as quickly as possible (because I don't have that much time) and then add features as needed.

Create a web application to display data in a flash card format.  With the intent of assisting in learning via repetition.

# The architecture

The front end is going to be done using React with Typescript and the server is just going to expose a CRUD API using what I like to Call Jimmy MVC using C#, ASP.net MVC 5, MediatR, StructureMap, Automapper and Fixie.  Later will migrate to .net Core but the learning curve is higher and I want to learn the language now :)

# Front End first
I have always been a business logic first kind of guy as I think the business logic is of greater value than the presentation layer.  If I think of the application as if it were cosonsle application I tend to break things into more logical and testable units.  But I have now officially changed my position.  The user experience is the only real interface with the client that they understand.  And if they have no UX then they you have done nothing from their view.  Client feedback is important so lets give them something to see into our world as quick as possible.  Hopefully we can accomplish both.

# Design simplest thing first

I use my free account at [moqups](http://moqups.com) and create a simple flash card mockup.

Simpliest thing first.  I want to show the English word the thai word and the transilteration (try to phonetically spell it with english characters.  For this blog I use Mango transliteration)

![](/content/images/2016/06/Mock1.png)

# Create git hub repository

https://github.com/StevenTCramer/ReactFlashCard

ReactFlashCard

# Create the project

We could start with just a file based html and js but the end architecture isn't that hard to get set up and will save us time.  See the post on Creating a base JimmyMVC5 template for step by step guide or just download the vsix.

# Create 













