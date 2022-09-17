Title: How to Build a Web Browser in 3 Weeks
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

For the sake of argument, let's say you decide to do something really stupid and you want to build a web browser. But you need to get it done in 3 weeks. How do you do it?

Easy. Just use Scrum. Scrum focuses on delivering end-user facing pieces of functionality in short sprints. And since the only practical end-user facing functionality of a web browser is to load and display a web page, you simply write one story as follows:

"As a user, I can load and view a web page, so that I can explore the world"

You assign that to one of your scrum teams, and since they work it two or three week sprints, you simply harangue them into "committing" to it, and presto, three weeks later it will be done. Easy.

Now as Donald Trump would say, "Obviously I'm being sarcastic ... but not that sarcastic to be honest with you."

But I use this example to make a point about what I've spent a lot of time lately thinking about ... the "shape" of software deliverables. Scrum proponents insist that all work on a project can be shaped vertically, and delivered as such. This seems to imply that the user interface, the middle-tier, and the back-end are all about the same size. Simply make those vertically sliced tasks thin enough and you can deliver them in short iterations.

Now this is a valuable mental model, and with a sincere effort, it can be made to work in a lot of cases. In something like an N-Tier, database-supported, web-based application, that ends up being close to true a lot of the time. But I don't think it's a fully generalizable concept, and I picked this example on purpose to illustrate why not.

I think most software, even the individual features, are shaped more like an iceberg. The user interface, the part that actually delivers the value to the user, is like the tip of the iceberg above the surface. Underneath the water is all the infrastructure to make it happen. And what do product managers want? They just want you to build the tip.

I picked the web browser as my example because it was the best example of an iceberg. The actual end user value, being able to see the web page, is the only exposed piece of functionality. It's almost mundane. But to actually make that happen requires a massive amount of infrastructure, including the network communication, HTML parsing, the internal DOM representation, the JavaScript parser and interpreter, the flow engine, and the paint engine.

Now you absolutely can build a browser in small pieces, no doubt. But will every one of those pieces truly represent end-user value? That would be pretty tough.

The thing that frustrates me most about scrum people is that they simply insist you can deliver it in vertical slices if you try hard enough. But they don't offer up a whole lot of practical advice on how to actually do it with any other than toy projects.

So I'm going to use this example as the basis of a thought experiment over the next few blogs to see if an iceberg like this could truly be done with scrum, and if so, how?

