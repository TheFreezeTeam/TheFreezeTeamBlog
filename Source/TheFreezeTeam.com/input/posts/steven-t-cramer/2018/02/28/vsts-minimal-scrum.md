---
DocumentName: vsts-minimal-scrum
Title: Visual Studio Team Services (VSTS) Minimal Scrum Template
Published: 02/28/2018
Tags: 
  - C#
  - Agile
  - Scrum
  - Azure DevOps
Author: Steven T. Cramer
Description: That definition is generic enough to mean almost anything or nothing and in practice that is often how it turns out. This blog is to explain our starting point with the VSTS templates.
Excerpt: That definition is generic enough to mean almost anything or nothing and in practice that is often how it turns out. This blog is to explain our starting point with the VSTS templates.
---

["Scrum is an Agile framework for completing complex projects."](https://www.scrumalliance.org/why-scrum)
That definition is generic enough to mean almost anything or nothing and in practice that is often how it turns out.

This blog is to explain our starting point with the VSTS templates.

The Scrum and Agile templates have a bunch of attributes. Do we need them to start our project?  No.

Will we need them later?  Maybe.

The following is how we modify the templates.

# Create an Inherited Process

Go to your VSTS site. `https://{accountname}.visualstudio.com/`
Click on the Visual Studio Icon in top left of the page to get to the root of the site if you are not there already.

Select the gear icon from the menu then select `Process`.

From the `...` menu at the right of the Scrum template select `Create inherited process`:

Give it a name: `MinimalScrum`

Click on the new process.

## Modify the layouts

We hide everything to do with estimates.
In a future post we will build our `Definition of Ready`,
but we attempt to make `User Stories/Product Backlog Items` small.
We consider every story the same.
We will use our history over time to determine how many stories we can do in a sprint,
thus allowing for planning and decision making.

> For thoughts on [#NoEstimates ](https://plan.io/blog/noestimates-6-software-experts-give-their-view/) or just google it.

A good `Definition of Ready` is a higher priority.
So to start a greenfield project I recommend you don't bother estimating and spend more time focusing on proper user
stories, automation and coding.

The resulting layouts are:

### Bug

<!-- ![](2018-02-24_1936.png) TODO: Cramer Missing Image from Ghost Migration  -->

### Epic

<!-- ![](2018-02-24_2007.png) TODO: Cramer Missing Image from Ghost Migration  -->

### Feature

<!-- ![](2018-02-27_2212.png) TODO: Cramer Missing Image from Ghost Migration  -->

### Impediment (not modified)

<!-- ![](2018-02-24_2010_001.png) TODO: Cramer Missing Image from Ghost Migration -->

### Product Backlog Item

<!-- ![](2018-02-24_2010.png) TODO: Cramer Missing Image from Ghost Migration -->

### Task

<!-- ![](2018-02-24_2011.png) TODO: Cramer Missing Image from Ghost Migration -->

## States

Hide the `New`, `Approved`and `Commited` states and add a `Draft`, `Submitted`, `Ready` and `Forecasted` states.

**Draft**: I am just creating this Item. Everyone else can ignore it until I am done.  At which time I will move it to `Submitted`

**Submitted**: The original author of the item believes the item meets the `Definition of Ready`, and is requesting the item to be triaged. If the Product Owner agrees that the item is ready they will move it to the `Ready` state if not they will move it back to the `Draft` state with comments as to why it is not ready.

**Ready**: The Item meats the `Definition of Ready` and can now be prioritized and put into a Sprint.

**Forecasted**: https://www.scrum.org/resources/commitment-vs-forecast

##### Bug States

* Draft
* Submitted
* Ready
* Forecasted
* Done
* Removed

##### Epic States

* Draft
* Submitted
* Ready
* In Progress
* Done
* Removed

##### Feature States

* Draft
* Submitted
* Ready
* In Progress
* Done
* Removed

##### Impediment States

* Open
* Closed

##### Product Backlog Item States

* Draft
* Submitted
* Ready
* Forecasted
* Done
* Removed

##### Task States

* Draft
* Submitted
* Ready
* In Progress
* Done
* Removed

Checklist:

There is a nice extension component that adds a [checklist control](https://marketplace.visualstudio.com/items?itemName=mohitbagra.workitem-checklist).  Checklists are simple control mechanisms that produce great return for the effort. I suggest using this for the `Definition of Done` and any other "sub tasks" one needs.

So there you go quick minimal Scrum. Build your own so your next project is ready to go.
