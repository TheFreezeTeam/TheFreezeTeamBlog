Title: The JavaScript Madness Part 5
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

**User Story:** 

 * As a developer I want to easily launch the starting of my development server. ("so that it saves time" is implied.)

**Analysis and task breakdown**:

> Yarn like npm simplifies/shortens the starting of the server using a scripts section in the project.json file. Reviewing the ejected `cra` project we see they actually execute a few different scripts.  Like the `cra`, I think it wise to create separate folders for the tooling scripts, application code, the webpack results, the configurations and a public folder that holds the files that we expect to serve directly.  We are going to add these tasks to this user story also.

**Task-1**

> Cory House uses a `tools` directory for tooling scripts.  The create-react-app (cra) uses a `scripts` directory.  I am going with the create-react-app directory structure as I think it will become the defacto standard.
> I hate abbreviations and thus would prefer `source` to `src` but again I will try to live with what I think `cra` is trying to establish as the baseline.

* Organize the folders to create tooling scripts separate from src.
 * Create `src` directory for application code
 * Create `scripts` directory for yarn scripts
 * Create `build` directory for webpack output
 * Create `public` directory for [files not be processed by webpack](https://github.com/facebookincubator/create-react-app/blob/master/packages/react-scripts/template/README.md#using-the-public-folder) index.html and favicon
 * Create `config` directory for the 

---

> Now that we have new directories we need to move the code and update the references.

**Task-2**

* Move `app.js` to `src`
* Update `webpack.config.js` to use `src` for input
* Update `webpack.config.js` to use `build` for output

---

**Task-3**

* Create `start.js` script in the `scripts` directory
 * Have `start.js` launched from yarn via `packages.json`
 * Have `start.js` start the `webpack-dev-server`

---

**Implementation**

**Task-1**

From `C:\git\jsm` Execute the following:
`mkdir src` 

`mkdir build`

`mkdir scripts`


```
C:\git\jsm
λ  ls


    Directory: C:\git\jsm


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----       12/27/2016   4:24 PM                build
d-----       12/27/2016   2:44 PM                node_modules
d-----       12/27/2016   4:24 PM                scripts
d-----       12/27/2016   4:24 PM                src
-a----       12/25/2016   1:46 PM             93 app.js
-a----       12/25/2016  10:36 AM           1482 bundle.js
-a----       12/24/2016  10:54 PM            155 debug.log
-a----       12/25/2016   2:03 PM            233 index.html
-a----       12/27/2016   2:44 PM            184 package.json
-a----       12/25/2016  10:29 AM             88 webpack.config.js
-a----       12/27/2016   2:44 PM          62549 yarn.lock
```

Task Complete

---

**Task-2**

Execute the following:

`mv app.js src`

`mv .\index.html src`

> Now the files are where we want them we need to update webpack.config.js

Inside VSCode update webpack.config.js to 

```

```
---

**After Thoughts**
