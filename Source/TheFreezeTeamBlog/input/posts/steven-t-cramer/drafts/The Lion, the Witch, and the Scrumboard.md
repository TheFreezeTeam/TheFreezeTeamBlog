Title: The Lion, the Witch, and the Scrumboard
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

I have no problem with many of the principles of Scrum/Agile. We can't define everything ahead of time. Check. Work in stable teams. Check. Two or three-week iterations. Check. An emphasis on working software. Check. Personal responsibility and ownership. Check.

But here's the biggest problem I have with scrum: the emphasis on complete, end-to-end, vertical slices of functionality as the **only** way to measure progress on the project is fundamentally at odds with how I've been trained to write software. I've been trained to break problems into smaller pieces, compose a sensible object-oriented class hierarchy, and write small, reusable classes that follow the SOLID development model.

Thinking about it visually, if I'm at the bottom of a sheer cliff wall, thinking about how to get to the top, I will probably think about how to build a ramp that I can walk up like a staircase. If a gentle, sloping staircase isn't possible, then I will at least think about cutting in some nice, (ahem) solid, dependable footholds that I can use to pull myself up inch-by-inch. With scrum, I feel like I'm asked to take one big leap up. And I damn well better get it done in one sprint.

I can think of three reasonable starting points that I might take with my SOLID mindset for any given user story.

***Data-Model-First***: With this approach, I would start by designing the data model. Here, I'd probably over-design it just a little, so that the data model always had at least as much information stored in it that the UI developers could possibly want. I'd design good indexes so that any developer could slice and dice the data model anyway they'd want without needing to continually ask me for changes. I'd review the data model with the stakeholders to ensure it could supply the correct information to the UI. I'd write the code that creates the schema. I'd document the schema, and I'd provide some sample queries that anyone could perform with the low-level database tools. And I may even write a development tool to load up the database with sample data. Sure, I probably wouldn't get everything exactly right and we'd have to revisit the data model from time-to-time to add in a field or two that was forgotten. But I'd have a pretty good foothold for making forward progress with the rest of the user story.

***API-First***: Here, I'd imagine all of the questions that the UI developers would reasonably want to ask of the system, and I'd design a coherent API to provide answers to those questions. I'd document the API and provide good sample queries for the developers to use with either curl or Postman. Since no persistence model existed yet, I'd mock up some data in a JSON file and connect it to the API.

***UI-First***: A third logical approach is to think about the user interface first. I'd design the screens and workflows, use a good UI mockup tool to create a neat, but low-fidelity picture of how it would look, and once again, get buy-in from the stakeholders. Knowing what the UI will look like will let me design a data model and an API that will feed into it.

So what's the problem? There's nothing in Scrum that says that these work items can't be put into a user story. In fact, if you look at the task breakdown of a typical user story, these are the kinds of tasks that you will typically see.

The problem is that I find it very difficult to do all of these things in a typical user story in a single sprint without sacrificing something important. You usually end up rushing all three of these tasks in order to fit the story into one sprint, and the result is that all three pieces of work are half-baked.

On my project, we have certain rules that we must follow:

* All user stories must be complete, end-to-end pieces of functionality that deliver demonstratable, end-user value.
* User stories must be completed in a single sprint, and teams are required to "commit" to completing the stories they chose for that sprint.
* Stories must be fully developed and tested before they can be demoed at the sprint demo.

These requirements, while good in principle, are too rigid in practice. We can resolve the stalemate by relaxing these requirements from time-to-time. Here's how it could work if the management team decided to be cooperative and empathic.

* **User stories don't always need to be end-user focused**. Sure, it would be part of an epic that was end-user focused, but the individual pieces wouldn't have to deliver end-user value by themselves. Then, I can take the proper amount of time to develop a data model, API, and UI. I want credit for making forward progress with each of these pieces. Don't tell me I haven't moved the needle just because it isn't all wired up yet.
* **User stories can span multiple sprints**. Fine, you don't like my idea of splitting a single end-user focused epic into three internally valuable, yet not fully end-to-end stories. You insist on forcing me into a larger story that is Data Model + API + UI all in one. Okay, then at least don't make me deliver every single story in one sprint, and don't force me into committing to something that has too many moving parts.
* **Let me demo the individual pieces of the story**. I can demo the Data Model using some sample data and the database's low-level command line tool. I can demo the API with curl or Postman. I can demo the UI with mockups or a live prototype with some mock data. Being able to demo these pieces individually gives me confidence that I'm on the right track and I'm making forward progress. Don't forbid me from demoing this forward progress to the team and obtaining feedback on it.

In my team, however, we aren't allowed to employ any of those tactics. The only management-supported technique for fitting a big story into multiple sprints is to make the stories thinner.

I understand the emphasis on wanting everything wired up and working together. Sometimes subcomponents work individually but fail an integration test. Hey, I get it. But have a little faith, will ya. You know, we are smart people. We do know how to bring individual pieces together into a working whole, and we will get it working. We have a much better chance of getting the pieces working together if we have confidence that each piece has been well designed, carefully implemented, and unit tested.

The 747 wasn't built with vertical slices. Neither was Windows, Linux, or any other reasonably complex piece of software. So stop it already with this rigid adherence to end-user focused, fully complete, end-to-end integrated software committed to and delivered in single sprints. It's just senseless. That is what I call oppressive agile.

For me, it's all about traction and momentum. I'm always looking for some way to get my hooks into the problem, gaining traction and momentum along the way. But I can't do that when I'm forced to create ultra-thin stories with no reasonable architectural scaffolding.


