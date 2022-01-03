DocumentName: the-javascript-madness-part-2
Title: The JavaScript Madness Part 2 - Execute js file from command line
Published: 02/18/2018
Tags: 
  - nodeJs 
  - Javascript 
Author: Steven T. Cramer
Image: Madness.webp
Excerpt: Execute js file from command line using node.
Description: Execute js file from command line using node.
---


# Ready, Set, JavaScript 

>Let's build some velocity. Start simple. Real simple.

**User Story**:  As a developer, I want to be able to execute a JavaScript file from the command line that displays a message.

**Analysis and task breakdown:**

>We need to create a js file that we want to execute.

**Task 1:**

* Create app.js

---

**Task 2:**

* Determine command line to execute above file.

---

**Implementation:**

**Task 1:**

>I keep all my [git](https://git-scm.com/) repos at `\git` to keep paths as short as I can.

Make a `jsm` directory under `C:\git` with `mkdir jsm`.  
Change into the new directory `cd jsm`
Open the directory in VSCode with `code .`

```
cd c:\git
C:\git
λ  mkdir jsm
    Directory: C:\git
Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----       12/24/2016   6:01 PM                jsm
C:\git
λ  cd jsm
C:\git\jsm
λ  code .

```

Add new file `app.js` with the following content:
```
console.log("Hello World!");
```

Task 1 Complete

---

**Task 2**

> node takes the JavaScript file to execute as the parameter.

Execute our app.  From the console by entering `node app.js`

```
λ  node app.js
Hello World!
```
User Story, Success!  Please forgive me if I don't write a test.  We have not gotten that far yet.




