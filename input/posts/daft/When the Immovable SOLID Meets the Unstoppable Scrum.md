Title: When the Immovable SOLID Meets the Unstoppable Scrum
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

I started on a new career opportunity a couple of years ago with a company that had adopted a strong Scrum workflow. Now mind you, I've had plenty of experience with agile methodologies. Everything thing I've done in my career has been agile is one way, shape, or form, even before the term "agile" was coined.

I thought I knew what agile was. But the specific implementation of agile, Scrum, with all of it's ethos, had never really be driven home for me. And I have to be honest, it was, and still is, a big adjustment for me.

The biggest mental adjustment I've grappled with is that Scrum in many ways contradicts the SOLID, clean code mindset that I've been raised with. Some days I think that Scrum violates every single software engineering best practice that I've ever learned.

But like it or not, Scrum is here to stay with us for quite a while. Sooner or later, us SOLID thinkers are going to have to learn to work within a Scrum framework.

Here's a few specific areas that I'm still thinking about:

**Is there any such thing as a vertical slice?**
Look at the world at large. Can you honestly think of any company that delivers a true, end-to-end product from raw materials all the way to consumer finished goods, such as Scrum purports to do with "vertical slices"? The world's economy operates on the concept of a value-chain. The output of one link in the value-chain becomes the input for the next. Can software really be any different than that? Any software project uses numerous third-party libraries to perform discrete tasks. Is it realistic for the internal organization of a software project to be different than that? Isn't dividing a large project into discrete, specific-responsibility subsystems the right way to build a complex system? And if so, is it realistic to think that one can simultaneously modify all subsystems in a single three-week sprint to fully implement a specific, end-user-visible feature? Is this really the best way to organize the work and produce a high-quality result?

**How do you handle pieces of the software that require specialized knowledge?"**
Suppose I'm writing a new web browser, and I need an ultra-high performance JavaScript engine to make it a market success. This will be a complex, delicate piece of software with highly specialized data structures and processing pipelines. Is it realistic to think that any ole developer from any ole scrum team is just going to helicopter into the middle of this thicket of code, add a "feature", and then go back to their other tasks? The JavaScript engine in and of itself does not consitute an end-user-visible feature. How are you supposed to fit this into a Scrum ethos that demands that end-user-visible functionality be delivered every three weeks.

* Does everything need to be end-user-visible?
* How do you build cognitive momentum?



