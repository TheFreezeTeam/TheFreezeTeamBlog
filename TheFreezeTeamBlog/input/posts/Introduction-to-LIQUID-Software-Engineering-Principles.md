Title: Introduction to LIQUID Software Engineering Principles
Tags: 
  - SOLID
Author: Steven T. Cramer
Image: LIQUID.png

---

So you've probably heard of the [SOLID](https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)) acronym for programming. But perhaps you haven't heard of a little known, but frequently practiced coding style which we affectionately call LIQUID. Let's take a moment to review the principles of LIQUID programming, for those less familiar with it (though we're sure it will ring a bell as we describe it). 

<table>
 <tr>
  <td>**L**</td>
  <td>Large: Write large files that contain long classes and methods</td>
 </tr>
 <tr>
  <td>**I**</td>
  <td>Interdependencies: Create obscure, fragile dependencies between modules</td>
 </tr>
 <tr>
  <td>**Q**</td>
  <td>Quagmire: Place unrelated items together into one file or class</td>
 </tr>
 <tr>
  <td>**U**</td>
  <td>Undocumented: Never document requirements, architecture, design, classes, or methods</td>
 </tr>
 <tr>
  <td>**I**</td>
  <td>Illegible: Keep code as illegible as possible by not following consistent coding standards</td>
 </tr>
 <tr>
  <td>**D**</td>
  <td>Duplicate code: Copy and paste similar functionality as-needed, and don’t worry about DRY coding</td>
 </tr>
<table>

***Large***<br />
Show off your intelligence by writing long, hard-to-follow classes and methods. You must be smart, because only a genius could possibly understand what it does. A side-benefit is that it will make you look good by slowing down any of your co-workers who may try to look at the code later. Be sure to group your long classes and methods into an even larger file, which will all but guarantee that merge conflicts arise the next time someone tries to merge your code into the development branch.

***Interdependencies***<br />
In order to ensure a continuous supply of work, be sure to create obscure, fragile dependencies between modules. This works especially well when those dependencies are also undocumented. The QA department will never run out of work as they continuously discover new corner cases that cause the code to break. And in the event that someone actually manages to fix a bug, the fix will almost certainly create at least one new bug in its place.

***Quagmire***<br />
Creating well-defined modules of single responsibility is a hassle. Just throw everything into one class whenever you can. This will let you use scrollbars as the Almighty intended, thereby undermining the silly attempts by many modern IDEs to provide intelligent navigation of source code. And, with today’s monitors getting larger and larger, you want to be sure to have files that are large enough to overwhelm even the largest displays on the market, especially since some clever engineers like to turn their monitors in portrait mode.

***Undocumented***<br />
Sure, documentation might save time in the long run, increase the quality of your code, and make it easier and faster to add new team members in the future, but don’t bother; too much trouble. Leaving things undocumented also maximizes the effect of the other LIQUID engineering principles.

***Illegible***<br />
Line things up neatly, keep your indent sizes consistent, and adhere too rigidly to team style guidelines and pretty soon your code will start looking like a scene out of Stepford Wives; too old-fashioned and boring. Where’s the excitement in that? Your code should have a certain amount of “cozy clutter”. If you’re out of ideas to liven things up, try using embedded tabs and making sure the team is using at least three different text editors with three different tab sizes. If you must insert comments in the code, be sure to make them obsolete, and use plenty of magic numbers and obscure variables names.

***Duplicate code***<br />
Don’t bother with DRY programming. It’s a waste of time since someone else will probably come along and mess up your efforts anyway. You’ve heard the adage “favor composition over inheritance.” Use neither. Copy-and-paste is faster. After all, if you need to fix a bug, wouldn’t you rather fix it in five different places? It’s a good way to show the boss you’re being productive.

As we here at TheFreezeTeam always strive to stay on top of the latest trends in modern software development, please leave a comment, or [send us](mailto://feedback@thefreezeteam.com) some examples of your LIQUID projects.



