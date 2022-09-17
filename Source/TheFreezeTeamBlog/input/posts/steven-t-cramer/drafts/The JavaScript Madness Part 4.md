Title: The JavaScript Madness Part 4
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

**User Story:** 

 * As a developer I want to be able to generate a list of licenses used by the npm dependencies.

**Analysis and task breakdown**:

> Good thing we looked at that create-react-app package list and figured this one out.  Yarn to the rescue.  This required a refactor from using the default npm client to using yarn.  Hopefully, you couldn't tell that the original version of the previous blogs was using npm.

**Task-1**

* Install Yarn

---

**Task-2**

* Update Environment documentation to install yarn.

---

**Task-3**

* Replace references in documentation to npm with appropriate yarn command. 

---

**Task-3**

* Document command to generate licenses.

---

**Implementation**

**Task-1**

> Reading the documentation on [yarn](https://yarnpkg.com/en/docs/install#windows-tab) and given our VM is Windows 10.

Download the [msi](https://yarnpkg.com/en/docs/install#windows-tab) and execute.

```
λ  yarn --version
0.18.1
```

> That actually was easy.

Mark Task-1 as Done

---

**Task-2**

> I will update the earlier blogs to say Yarn now. (Done)

> I also had to redo the commands and capture the new output so as not to confuse anyone more.  Added a Editor Note at bottom of the page.

> This is like editing the book while you are reading it.

---
**Task-3**

> The [yarn docs](https://yarnpkg.com/en/docs/cli/licenses) make this rather clear.

`yarn licenses ls` 

>>Running this command will list, in alphabetical order all of the packages that were installed by yarn or yarn install, and give you the license (and URL to the source code) associated with each package.\

`yarn licenses generate-disclaimer`

>>Running this command will return a sorted list of licenses from all the packages you have installed to the stdout.

Mark Task Done.

---

**After Thoughts**

> Now that we have yarn working let's shorten up the call to start the dev server. using yarn scripts




