Title: The JavaScript Madness Part 3
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

* as a developer I want to initiate, from the command line, the loading of an HTML page into a browser.

**Analysis and task breakdown**:

>First I need an HTML page to show in a browser.

**Task-1**

* Create HTMLfile.

---

> We need something to serve the html page.  We could use "file-based" for the minimal but we know we will need more. So looking at the `cra` app we see they are using [webpack-dev-server](http://webpack.github.io/docs/webpack-dev-server.html). Let's go with that.

**Task-2**

* Install webpack-dev-server

---

>So we chose a dev server but how do we start it and have it show the page we created? Reading the [Readme](https://github.com/webpack/webpack-dev-server) it looks like we can call it from the command line.

**Task-3*** 
> build command to launch and point to our page.

---

**Implementation**

**Task-1**

inside visual studio code create `index.html` as follows:
```
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>The JavaScript Madness</title>
</head>
<body>
  <div>Welcome to the Madness</div>
</body>
</html>
```
To test this execute `ii index.html` in powershell.
>>`ii` is alias for `invoke-item`

>It works! 

Move Task 1 to done.

---

**Task 2**

>>The webpack-dev-server is a little Node.js Express server, which uses the webpack-dev-middleware to serve a webpack bundle.

>Here we want to install our first npm package but before that, we need to initialize the project. 

Execute `yarn init -y`.  This will create a package.json file with all the default settings.

```
λ  yarn init -y
Wrote to C:\git\jsm\package.json:

{
  "name": "jsm",
  "version": "1.0.0",
  "main": "index.js",
  "license": "MIT"
}
```
>The `package.json` file is used primarily to manage all the projects dependencies. We will see that yarn also does more, later. For more info see the [yarn docs](https://yarnpkg.com/).

From the console excute `yarn add --dev webpack-dev-server`

```
λ  yarn add --dev webpack-dev-server
yarn add v0.18.1
info No lockfile found.
[1/4] Resolving packages...
[2/4] Fetching packages...
[3/4] Linking dependencies...
warning Unmet peer dependency "webpack@1 || ^2.1.0-beta || ^2.2.0-rc.0".
warning Unmet peer dependency "webpack@>=1.3.0 <3".
```
> yarn installed a bunch of items into a folder called `node_modules` but notice it did not install the webpack dependency. (why?  Another thing I don't know. It says you need it and it manages the packages, why not install it also?)

Execute `yarn add --dev webpack`

> `-dev` indicates this is not needed for the production environment but is only needed in development.

>this time we strangely still get a warning but looking in node_modules webpack was installed.  This [bug](https://github.com/yarnpkg/yarn/issues/2132) is on the yarn issues list.

>yarn updates the package.json as follows:

```
{
  "name": "jsm",
  "version": "1.0.0",
  "main": "index.js",
  "license": "MIT",
  "devDependencies": {
    "webpack": "^1.14.0",
    "webpack-dev-server": "^1.16.2"
  }
}
```
Task 2 Complete

---

**Task 3**

>We want to launch the webpack dev server and serve up our HTML file.  How do we do that?  Read the [Geting started](https://github.com/webpack/webpack-dev-server) to see if it helps.

>>"The easiest way to use it is with the CLI. In the directory where your webpack.config.js is, run: node_modules/.bin/webpack-dev-server"

>So what the heck is this `webpack.config.js` file of which they speak?  I guess we better read the webpack docs a bit to figure out what that is?

---

##### So what is this webpack thing?

>**webpack** is a tool to bundle your JavaScript files together. Instead of loading many files one by one you can bundle them together.  Maintaining one giant file would be a bad idea for a team. But shipping a minified bundle to the browser is a good idea.

> `webpack.config.js`, in its most basic form, tells webpack what files you want to bundle and where you want it to put the bundle when complete.

----

>We currently only have app.js and a single html file.  So let's not bundle anything.

execute: `.\node_modules\.bin\webpack-dev-server`

```
C:\git\jsm
λ  .\node_modules\.bin\webpack-dev-server
Hash: 396f0bfb9d565b6f60f0
Version: webpack 1.14.0
Time: 44ms
webpack: bundle is now VALID.
 http://localhost:8080/webpack-dev-server/
webpack result is served from /
content is served from C:\git\jsm
```

>I don't see an error, and the cursor didn't return. Something must still be running. Let's try that url from the above output.

goto (http://localhost:8080/webpack-dev-server/)

>Wow it shows my page and has a header that says "App Ready"

>We are making progress but where did that "App Ready" come from? Maybe we better read the [docs](http://webpack.github.io/docs/webpack-dev-server.html).

>... time lapse to read....

>I guess we can run in `inline mode` or `Iframe mode`.  The URL we are using with the App Ready is in `Iframe mode`.  The docs claim it can automatically refresh the page. That sounds cool, let's change something. 

>The only thing we are viewing is a static HTML page.  

Change the div to  `<div>Welcome to the Madness Change</div>` and save.

> Upon saving the file we should see a change, right?

> Hmmm ... I guess not. 
 
Press F5

> Now I see the change. But that isn't automatic refresh. What gives?

>I am guessing that only the JavaScript file changes, cause auto reload?  So let's add in the app.js to the Html page and have it change something in the HTML that we can see.  Then try it.

Update `app.js` as follows:
```
document.getElementById("app").innerText = "Javascript changes the document on load.";
```

Update `index.html` to add an id to the div and include the script:

```
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>The JavaScript Madness</title>
</head>
<body>
  <div id="app">Welcome to the Madness Change</div>
  <script src="bundle.js"></script>
</body>
</html>
```
Press F5

>We see the new page.  Now if we change the js and save will it auto refresh? ....... NO!

>Maybe we actually need to create a bundle that changes to trigger it?

>To create a bundle we need that config file the docs were talking about.

At the root of the project create `webpack.config.js` as follows:

```
module.exports = {
  entry: './app.js',
  output: {
    filename: 'bundle.js'
  }
}
```

>This config tells webpack to start with `app.js` and bundle all of its dependencies into a single file named `bundle.js`

>Let's see if webpack creates a bundle.
 
Execute `webpack` from powershell.

```
C:\git\jsm
λ  webpack
webpack : The term 'webpack' is not recognized as the name of a cmdlet, function, script file, or operable program.
Check the spelling of the name, or if a path was included, verify that the path is correct and try again.
At line:1 char:1
+ webpack
+ ~~~~~~~
    + CategoryInfo          : ObjectNotFound: (webpack:String) [], CommandNotFoundException
    + FullyQualifiedErrorId : CommandNotFoundException
```

>We get an error!

>This is because the webpack command is located under `node_modules\.bin`.  We will make this shorter using yarn scripts later.  For now, 

Execute `.\node_modules\.bin\webpack`

```
C:\git\jsm
λ  .\node_modules\.bin\webpack
Hash: 3e6155a0ecb00b79a3fd
Version: webpack 1.14.0
Time: 72ms
    Asset     Size  Chunks             Chunk Names
bundle.js  1.48 kB       0  [emitted]  main
   [0] ./app.js 94 bytes {0} [built]
C:\git\jsm
λ  ls


    Directory: C:\git\jsm


Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----       12/24/2016   9:27 PM                node_modules
-a----       12/25/2016  10:18 AM             94 app.js
-a----       12/25/2016  10:36 AM           1482 bundle.js
-a----       12/24/2016  10:54 PM            155 debug.log
-a----       12/25/2016  10:14 AM            230 index.html
-a----       12/24/2016   9:27 PM            305 package.json
-a----       12/25/2016  10:29 AM             88 webpack.config.js
```

>Webpack created a `bundle.js` 

>Let's use the bundle in the HTML.

Replace `app.js` with `bundle.js` in `index.html`:

```
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>The JavaScript Madness</title>
</head>
<body>
  <div id="app">Welcome to the Madness Change</div>
  <script src="bundle.js"></script>
</body>
</html>
```

>In order for the webpack-dev-server to pickup the changes to the config we need to stop it and restart.  

Press `Ctrl-C` to stop and confirm with `'Y'`.  
Then rerun the command (up arrow) to start webpack-dev-server.

```
C:\git\jsm
λ  .\node_modules\.bin\webpack-dev-server
Hash: 396f0bfb9d565b6f60f0
Version: webpack 1.14.0
Time: 44ms
webpack: bundle is now VALID.
 http://localhost:8080/webpack-dev-server/
webpack result is served from /
content is served from C:\git\jsm
^CTerminate batch job (Y/N)? Y
C:\git\jsm
λ  .\node_modules\.bin\webpack-dev-server
 http://localhost:8080/webpack-dev-server/
webpack result is served from /
content is served from C:\git\jsm
Hash: 7a71ea8b93fc1ba8cd98
Version: webpack 1.14.0
Time: 100ms
    Asset     Size  Chunks             Chunk Names
bundle.js  1.47 kB       0  [emitted]  main
chunk    {0} bundle.js (main) 86 bytes [rendered]
    [0] ./app.js 86 bytes {0} [built]
webpack: bundle is now VALID.
...
```

>Now that dev server is running using the webpack bundle which was built using the webpack config, Let's change the `app.js` file and see if the browser auto refreshes.

Update `app.js` as follows:
```
document.getElementById("app").innerText = "Javascript changes the document on load. Change";
```

Save the file
https://thefreezeteam.com/ghost/
>On Save does it change? YES!!! See that was simple.
>Guess what if you try it in Edge it will fail. An [open bug.](https://github.com/webpack/webpack-dev-server/issues/639)
It does work if you use the inline version.  I should fix this for them but given I have no idea how I will continue.  adding --inline option to the command line will work in  

---

> So we got the auto refresh to work but don't forget the task is to launch the server and open our page in the browser. (Boy we have surely blown our estimate on this task)

> Ok so how do we open?  Back to the [docs](http://webpack.github.io/docs/webpack-dev-server.html).

>I see what looks like something that may do the trick `--open`.

Stop the current running dev server `Ctrl-C Y`
Execute:  .\node_modules\.bin\webpack-dev-server --open

>This successfully launches the dev server and opens a browser to http://localhost:8080/webpack-dev-server/ if we don't want the status header ("App ready") we can add the --inline option as:

Execute:  .\node_modules\.bin\webpack-dev-server --open --inline

>This option opens http://localhost:8080/ and works with Edge and Chrome.

Task 3 complete.

**Retrospective**

> What to do next?  

>I want to write some code but also when I get to writing code I want minimal disruptions with tooling.  Maybe we should look through the dev dependencies of the `cra`app and start to define what each is and whether we want it or not?

> Notice that our command to launch the app in a browser is rather long. In the `create-react-app` they just call `npm start`.  So we will add that to the backlog.

> It is not lost on me that I am writing User Stories that start "As a developer."  Given we are attempting to build a template for a developer, they are the end user of our product.  When the developer using the template writes their user stories they will focus on their end users.

> A quick review of the packages list was scary.... what the heck is `chalk` what is `yarn`? 

> Wow yarn sounds interesting.  It is an improved npm client by the devs from Facebook, Google and more.  Should we use that instead of npm?  Trying to read the tea leaves here my gut instinct is yes.  One key reason is legal.  `yarn licenses generate-disclaimer` generates a disclaimer containing the contents of all licenses of all packages. Just last week when asked by management how we could get license information to legal, I stated that I hoped npm and Nuget had a way where we could script this.  I guess npm doesn't but yarn does.

> So already we are refactoring some tooling. replace npm with yarn. Let's do it.


```
Editor Note:  This originally referenced npm but The Javascript Madness Part 4 refactored this page to use yarn.  See it is madness.

```
