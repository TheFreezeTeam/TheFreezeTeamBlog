Title: Premature Optimization?
Tags: 
  - C#
Author: Steven T. Cramer
Image: PrematureOptimization.png

---

I am not fan of the "premature optimization" phrase.

## [Premature](https://www.google.com/search?q=Dictionary#dobs=premature): occurring or done before the usual or proper time; too early.

The phrase is subjective, and begs the question; "what is premature?" 
* Should I never fill up my car with fuel until I am out of gas? 
  > Honestly the car is running fine and I don't need gas at this point.  
* Should I never change the oil?  
* "I only have one version of this file why do I need version control?"
  
## Foresight is a GREAT thing not a detriment.

* You should fill up the car with fuel prior to it running out, in order to reduce the overall time in transit.
* You should change the oil in the car, to avoid engine damage which would eliminate the value of having car in the first place.
* You should use version control because the likely hood of having more than one version of files in your application is low. From experience we have learned the value of maintaining the history of all files.

## [Optimization](https://www.google.com/search?q=Dictionary#dobs=optimization): the action of making the best or most effective use of a situation or resource.

Given that definition how can optimization ever be done "too early"? 

**The phrase "premature optimization" is an oxymoron.** One can not optimize prematurely.

The term is intended to mean that the implementation is NOT the optimal.  The phrase is used to imply you are trying to solve a problem in a less effective manner.  Normally to indicate that you are trying to solve a problem that your application is unlikely to incur.

We write code that is very precise. Yet in our spoken and written language (e.g. English) we parrot oxymorons and memes. I believe with some awareness we can do better.

When is it optimal to switch from our current architecture to a different architecture? What are the driving forces behind caching, event sourcing, micro-services etc?

I like to flip the implied blame here. If the person is using an architecture in an inappropriate situation.  Then maybe the failure is in my ability to communicate my requirements and needs or the inability of the people behind the architecture to explain where it is best utilized.

It doesn't hurt to be skeptical. Skepticism drives one toward understanding. Event sourcing and micro-services are NOT your typical apps.  One should have a reason to implement them.  Being aware of the concepts is good and a good architecture will not preclude you from using them.  But I would recommend you don't start there without being very confident of the need.

References:

[Don't Let the Internet Dupe You, Event Sourcing is Hard](https://chriskiehl.com/article/event-sourcing-is-hard?__s=bcwnrs3bhsrct6tnonsh)
