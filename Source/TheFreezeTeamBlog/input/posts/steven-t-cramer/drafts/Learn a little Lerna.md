Title: Learn a little Lerna
Tags: 
  - C# 
  - Blazor 
  - dotnet 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

Packages packages everywhere.  Can Lerna help us?

The problem:

The hypothesis:

The example:

Create three packages.

1. UI package (reusable React Component)
2. mock api server
3. Todo App that uses UI
4. .net core server

The tools:

Lerna
Yarn
Typescript
Webpack
Babel
React
Jest

Step by step:

`npm install --global lerna`

Create repo on github with readme.md

```
git clone https://github.com/StevenTCramer/learn-a-little-lerna.git
cd .\learn-a-little-lerna

Run the following script steps:
lerna init
```
Edit the lerna.json file to use yarn as follows:

```
{
  "lerna": "2.0.0-beta.38",
  "packages": [
    "packages/*"
  ],
  "version": "0.0.0",
  "npmClient": "yarn"
}
```

C:\git\tft\learn-a-little-lerna\app\package.json
```

```

```
mkdir packages
cd packages
mkdir app
cd app
yarn init -y

cd ..
mkdir beta
```

```
C:\git\tft\learn-a-little-lerna [master ≡ +3 ~0 -0 !]
λ ls | tree /f
Folder PATH listing
Volume serial number is 068A-6A0E
C:.
│   lerna.json
│   package.json
│   README.md
│
└───packages
    ├───alpha
    │       index.js
    │       package.json
    │
    ├───app
    │       index.js
    │       package.json
    │
    └───beta
            index.js
            package.json
```

**C:\git\tft\learn-a-little-lerna\packages\alpha\index.js
```
module.exports = 'alpha'
```
**C:\git\tft\learn-a-little-lerna\packages\beta\index.js
```
module.exports = 'beta'
```

**C:\git\tft\learn-a-little-lerna\packages\app\index.js
```
var alpha = require('alpha')
var beta = require('beta')
console.log(alpha + " " + beta)
```

`lerna bootstrap`

```
C:\git\tft\learn-a-little-lerna [master ≡ +3 ~0 -0 !]
λ lerna bootstrap
Lerna v2.0.0-beta.38
Bootstrapping 3 packages
Preinstalling packages
alpha                                              ╢█████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░╟
app                                                ╢███████████████████████████████████░░░░░░░░░░░░░░░░░╟
beta                                               ╢████████████████████████████████████████████████████╟
Symlinking packages and binaries
alpha                                              ╢█████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░╟
app                                                ╢███████████████████████████████████░░░░░░░░░░░░░░░░░╟
beta                                               ╢████████████████████████████████████████████████████╟
Postinstalling packages
alpha                                              ╢█████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░╟
app                                                ╢███████████████████████████████████░░░░░░░░░░░░░░░░░╟
beta                                               ╢████████████████████████████████████████████████████╟
Prepublishing packages
alpha                                              ╢█████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░╟
app                                                ╢███████████████████████████████████░░░░░░░░░░░░░░░░░╟
beta                                               ╢████████████████████████████████████████████████████╟
Successfully bootstrapped 3 packages.
```

Execute the app as shown:
```
C:\git\tft\learn-a-little-lerna [master ≡ +3 ~0 -0 !]
λ node .\packages\app\index.js
alpha beta
```

# How to add a dependency using lerna:

```
Run npm show <pkg> version to determine what version to use
Manually add the dependency to package.json
Run lerna bootstrap --scope="<my pkg>"
or 
lerna bootstrap --concurrency=1
```

# Now let's add typescript

npm install -g typescript (if you want to install global. this is not required nor even recommended but does simplify commands)

Following the above procedure to update the app package.json to include typescript as a dev dependency. "typescript": "2.2.1"

```
{
    "name": "app",
    "version": "1.0.0",
    "main": "index.js",
    "license": "MIT",
    "dependencies": {
        "alpha": "1.0.0",
        "beta": "1.0.0"
    },
    "devDependencies": {
        "typescript": "2.2.1"
    }
}
```

rename `index.js` to `index.ts`

execute the TypeScript compiler (tsc) `.\node_modules\.bin\tsc index.ts`

```
C:\git\tft\learn-a-little-lerna\packages\app [master ≡ +4 ~0 -0 !]
λ .\node_modules\.bin\tsc index.ts
index.ts(1,13): error TS2304: Cannot find name 'require'.
index.ts(2,12): error TS2304: Cannot find name 'require'.
```

To solve the above error add the dev package `"@types/node": "7.0.8"`

Now, `tsc` will transpile the ts to a js file located next to the orginal.

Excute the transpiled should work just as before
```
G:\git\tft\learn-a-little-lerna\packages\app [master ↑ +4 ~4 -1 !]
λ  tsc index.ts
G:\git\tft\learn-a-little-lerna\packages\app [master ↑ +4 ~3 -1 !]
λ  node .\index.js
alpha alpha
```

## Add an html page

```
<!DOCTYPE html>
<html>
    <head><title>TypeScript app</title></head>
    <body>
        <script src="index.js"></script>
    </body>
</html>
```

open this html page in a browser (file:///G:/git/tft/learn-a-little-lerna/packages/app/index.html) and you will get the following error:

`Uncaught ReferenceError: require is not defined at index.js:1`

This application runs in node but will fail in a browser as node has a means to import modules and currently the browsers do not implement ES6 module loading.

To overcome this there are currently two options that I know of without adding other tools yet they both require module libraries:
* use requires.js
* use system.js

all of which require changing the tsc compiler options.  The compiler options can be passed in via the command line but it is much easier to use the tsconfig.json configuration file.  

Create a tsconfig.json file using the defaults with `tsc --init`

```
{
    "compilerOptions": {
        "module": "commonjs",
        "target": "es5",
        "noImplicitAny": false,
        "sourceMap": false
    }
}
```

We will want our transpiled and bundled file stored somewhere else let's go with a `dist` directory.

λ  .\node_modules\.bin\tsc index.ts --outFile ".\dist\app.js"

Let's update the compiler option to include the outFile

```
{
    "compilerOptions": {
        "module": "commonjs",
        "target": "es5",
        "noImplicitAny": false,
        "sourceMap": false,
        "outFile": "./dist/app.js"
    }
}
```
also update the html file to reference the new location.

attempting to compile results in the following error:

```
G:\git\tft\learn-a-little-lerna\packages\app [master ↑ +4 ~4 -1 !]
λ  tsc
error TS6082: Only 'amd' and 'system' modules are supported alongside --outFile.
```
Update the tsconfig.json as follows:

```
{
    "compilerOptions": {
        "module": "amd",
        "target": "es5",
        "noImplicitAny": false,
        "sourceMap": false,
        "outFile": "./dist/app.js"
    }
}
```

run tsc to transpile the javascript  and update the html to reference the new location

```
<!DOCTYPE html>
<html>
    <head><title>TypeScript app</title></head>
    <body>
        <!--<script src="dist/app.js"></script>-->
        <script data-main="dist/app" src="https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.3/require.min.js"></script>
        <script src="index.js"></script>
    </body>
</html>
```

update package.json to include a script


References: 
https://github.com/lerna/lerna/issues/53
https://github.com/reggi/lerna-tutorial
https://www.typescriptlang.org/docs/handbook/react-&-webpack.html
https://www.typescriptlang.org/docs/handbook/tsconfig-json.html
https://www.typescriptlang.org/docs/handbook/compiler-options.html
http://brianflove.com/2016/11/08/typescript-2-express-node/
