---
DocumentName: the-javascript-madness
Title: The JavaScript Madness Part 0 - Environment
Published: 01/15/2017
Tags: 
  - JavaScript
  - React
  - Visual Studio
  - VSCode
Image: Madness.webp
Author: Steven T. Cramer
Description: Create Azure Windows 10 VM with VS2017RC Size Standard DS2 v2  This should do. Add Data Disk to VM via Azure. 
Excerpt: Create Azure Windows 10 VM with VS2017RC Size Standard DS2 v2 This should do. Add Data Disk to VM via Azure. 
---

## Environment

---

### Operating System - Environment

* Create [Azure](https://portal.azure.com) Windows 10 VM with VS2017RC
  * Size: Standard DS2 v2 (2 cores, 7 GB memory) - This should do.
  * Add Data Disk to VM via Azure. 
  * [Expand azure hard drive to 1GB](http://www.thefreezeteam.com/2017/01/15/azure-resize-os-drive/)
  * [Consider automating the hours of operation to save credits/money](https://docs.microsoft.com/en-us/azure/automation/automation-solution-vm-management)
* Run Windows 10 `Check for updates` and install.
* Disable "Hide extensions for known file types"
* Enable "Show hidden files, folders, and drives"
* Run [VS2017RC installer](https://www.visualstudio.com/vs/visual-studio-2017-rc/) to update to latest versions
* Attach data disk as drive 
* Go to [nininte.com](https://ninite.com/) and install:
  * VSCode 
  * Chrome 
  * Notepad++ 
  * pin all 3 above to task bar.
* Open Chrome and set it as your default browser.
* Ensure [React devtools extention](https://chrome.google.com/webstore/detail/react-developer-tools/fmkadmapgofadopljbjfkapdkoienihi) is added to Chrome
* Ensure [Redux devtools extention](https://chrome.google.com/webstore/detail/redux-devtools/lmhkpmbekcpmknklioeibfkpmmfibljd?hl=en) is added to Chrome
* Install [yarn](https://yarnpkg.com/en/docs/install#windows-tab)

#### Cmder (Console Emulator)

* Download [Cmder](http://cmder.net/) with git-for-windows
* Extract into `Program Files\cmder` 
* Execute cmder.  
* In Cmder settings Configure PowerShell as default console. 
<!-- ![](2017-01-15_1720.png) TODO: Cramer Missing Image from Ghost Migration  -->

* Under Startup->Tasks [Remove the -No Profile from the PowerShell configuration](https://superuser.com/questions/956182/cmder-powershell-ignores-profiles).
<!-- ![](2018-03-15_2152.png) TODO: Cramer Missing Image from Ghost Migration  -->
* Select Keys & Macro and search for split. Set the following:
<!-- ![](2018-03-15_2150.png) TODO: Cramer Missing Image from Ghost Migration  -->
* Open Cmder in Powershell and `Install-Module posh-git`
* Pin Cmder to task bar or Start Menu.
 

## Dev-Environment

* Install [Node](https://nodejs.org/en/) 6.9.4 (for this blog)
 >Don't install 7 or higher until you are sure [webpack-dev-server](https://github.com/webpack/webpack-dev-server) support is compatible. 

Confirm Node is installed and in the path. Open Cmder and run `node -v`.  you should see the following:

```
λ  node -v
  v6.9.4
```
While at it what determine the version of npm and yarn? `npm -v`

```
λ  npm -v
3.10.10

λ  yarn --version
0.19.1
```

**What is node?**

> Node.js® is a JavaScript runtime built on Chrome's V8 JavaScript engine. Node.js uses an event-driven, non-blocking I/O model that makes it lightweight and efficient. Node.js' package ecosystem, npm, is the largest ecosystem of open source libraries in the world.

**What is npm?**

>npm is the package manager for JavaScript. Find, share, and reuse packages of code from hundreds of thousands of developers — and assemble them in powerful new ways.

**What is yarn?**
>Yarn is a package manager for your code. It allows you to use and share code with other developers from around the world. Yarn does this quickly, securely, and reliably so you don’t ever have to worry. We are going to use yarn in place of npm.

---

## VS Code Plugins

* C# `ext install csharp`
* TSLint `ext install tslint`
* ESLint `ext install vscode-eslint`
* Typescript React/Redux Snippets `ext install typescript-react-snippets`
* React Redux ES6 Snippets `ext install react-redux-es6-snippets`
* PowerShell `ext install PowerShell`

> let's write some code.

```
Editors Note: When you get to `The Javascript Madness Part 4 you will see we came back to this page and added all of thereferences to yarn. Refactoring is a good thing.
```
