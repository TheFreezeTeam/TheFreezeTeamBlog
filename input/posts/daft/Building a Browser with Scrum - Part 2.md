Title: Building a Browser with Scrum - Part 2
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

**Technique #1 - Use Open Source Components**

If your project requires large, complex subcomponents, such as a web browser does for the HTML parser, DOM manager, CSS manager, JavaScript engine, flow engine, paint engine, etc., you might be able to find some open source components that are close to what you need. If the component isn't an exact fit, you can fork it or contribute changes back to the original project.

With the big rocks in place, you can write vertically-sliced stories that enhance, refine, and integrate the components together. It may be easier to organize that kind of work into pieces that will be visible to the user, rather than doing all of the mass infrastructure work on behind-the-scenes components that are invisible to the user.

I was hesitant to mention this technique because it almost feels like cheating. It would be like me selling you a real-estate seminar on how to buy houses with no money down, and then telling you that the way to do it is to borrow money from your parents. Either way, the money has to come from somewhere. It's the same with open source projects. Whether it's your own project or an open source component, the work has to come from somewhere.

Even so, with so much open source software out there, it's wise to do a review of available open source software before embarking on any project.

For the sake of this exercise, I'm assuming we don't have the necessary components available to us. If I didn't make that assumption, then I could just tell you to start with Chromium or WebKit and you'd be a long way there.

**Technique #2 - Spin Off Components as Open Source Projects**

This technique is the inverse of the first. If you can identify major components of the software that feel more like development libraries that other developers will use to embed in their products, consider spinning those components off as independently-managed open source projects.

This technique suffers from the same self-referencing problem as the first: the work still has to come from somewhere.

Still, you get two big benefits right from the start. First, the management psychology is differnt because the chain of ownership and responsibility has been divided. Second, the downstream project now has an entirely different end-user. The end-user is other developers. Once you can turn other developers into the end-user, you'll be home free in writing user stories. You can simply define a nice, find-grained API, and go about delivering that API in small pieces. You no longer have to worry about why the end-user will care about the functionality.

The biggest danger I see in this approach is if the downstream project lags behind in delivering the functionality that the core

