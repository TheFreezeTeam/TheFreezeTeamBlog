---
DocumentName: TheFreezeTeam-reacts-to-typeScript
Title: TheFreezeTeam Reacts to TypeScript
Published: 04/10/2016
Tags:
  - React 
  - Typescript 
  - MVC5 
Author: Steven T. Cramer
Description: So let's write a simple hello world app.  Using Typescript 1.8 and React in Visual Studio 2015 Update 2. .Net 4.5.2 (MVC 5).
Excerpt: So let's write a simple hello world app.  Using Typescript 1.8 and React in Visual Studio 2015 Update 2. .Net 4.5.2 (MVC 5).
---

So let's write a simple hello world app.
Using Typescript 1.8 and React in Visual Studio 2015 Update 2. .Net 4.5.2 (MVC 5).

Make sure you have installed TypeScript 1.8.4 in Visual studio.
<!-- ![](2016-04-09_1405.png) TODO: Cramer Missing Image from Ghost Migration -->

#### Create Hello World Project
Create new project and select ASP.NET Web Application.
Choose a folder and name it HelloWorld, Of course :)
<!-- ![](2016-04-08_9-30-11.png) TODO: Cramer Missing Image from Ghost Migration  -->

Then choose MVC

<!-- ![](2016-04-08_10-01-59.png) TODO: Cramer Missing Image from Ghost Migration -->

Build and execute the application to confirm it works.

####Add Controller

Under controllers create new class named HelloWorldController

<!-- ![](2016-04-09_1421.png) TODO: Cramer Missing Image from Ghost Migration -->

<!-- ![](2016-04-09_1423.png) TODO: Cramer Missing Image from Ghost Migration -->

<!-- ![](2016-04-09_1424.png) TODO: Cramer Missing Image from Ghost Migration -->
```csharp
namespace HelloWorld.Controllers
{
  using System.Web.Mvc;

  public class HelloWorldController : Controller
  {
    // GET: HelloWorld
    public ActionResult Index()
    {
      return View();
    }
  }
}
```

In the controller code rick click on View and select Add View

<!-- ![](2016-04-09_1429.png) TODO: Cramer Missing Image from Ghost Migration -->

<!-- ![](2016-04-09_1430.png) TODO: Cramer Missing Image from Ghost Migration -->

Modify View as follows:

```html

@{
    Layout = null;
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>Index</title>
</head>
<body>
    <div id="content"> 
      Content here!
    </div>
</body>
</html>
```
Excute and verify

<!-- ![](2016-04-09_1433.png) TODO: Cramer Missing Image from Ghost Migration -->

#### Install React.Web.Mvc4 Nuget

<!-- ![](2016-04-09_1437.png) TODO: Cramer Missing Image from Ghost Migration -->

`install-package React.Web.Mvc4`

## Add Tutorial.jsx file

Right click on Scripts folder and select Add->Javascript File

<!-- ![](2016-04-09_1448-1.png) TODO: Cramer Missing Image from Ghost Migration -->

```jsx
var CommentBox = React.createClass({
  render: function() {
    return (
      <div className="commentBox">
        Hello, world! I am a CommentBox.
      </div>
    );
  }
});
ReactDOM.render(
  <CommentBox />,
  document.getElementById('content')
);
```

### Update Index.cshtml

Update the HTML to include the React scripts and our Tutorial.jsx as follows:

```html
@{
  Layout = null;
}

<!DOCTYPE html>

<html>
<head>
  <meta name="viewport" content="width=device-width" />
  <title>Index</title>
</head>
<body>
  <div id="content"></div>
  <script src="https://fb.me/react-0.14.0.min.js"></script>
  <script src="https://fb.me/react-dom-0.14.0.min.js"></script>
  <script src="@Url.Content("~/Scripts/Tutorial.jsx")"></script>
</body>
</html>
```

Build and execute the application.
<!-- ![](2016-04-09_1548.png) TODO: Cramer Missing Image from Ghost Migration -->
Congratulations you now have a React component.

#### Convert the JavaScript to TypeScript

Rename Tutorial.jsx to Tutorial.tsx

The dialog will appear.
<!-- ![](2016-04-08_10-15-29.png) TODO: Cramer Missing Image from Ghost Migration -->

Select no. 

Build the project and you should now see the following errors:
<!-- ![](2016-04-09_1510.png) TODO: Cramer Missing Image from Ghost Migration -->

First, let's address the "Cannot use JSX unless the '--jsx' flag is provided" error:

Go to the Properties on your project and select the `TypeScript Build` tab and for the `JSX compilation in TSX Files` and select `React`

<!-- ![](2016-04-09_1514.png) TODO: Cramer Missing Image from Ghost Migration -->

Now build and confirm that error is no longer.

#### Add the DefinitelyTyped React libraries

Create the typings and react folders under scripts: `Scripts/typings/react`

*The react DefinitelyTyped should be available from Nuget but Nuget did not have all the packages that we needed or were
out of date.*

*[Description of Issue here](https://github.com/DefinitelyTyped/NugetAutomation/issues/14)*

We therefore, went directly to [DefinitelyTyped git hub page](https://github.com/DefinitelyTyped/DefinitelyTyped) and 
downloaded the latest repo.

This repo contains a ton of stuff that you do not need for this blog. Extract the repo. 

Right click on the `Scripts/typings/react` folder and select `Add Existing Item` Browse to the extracted `react` folder 
select the following as shown in the image then Add.

<!-- ![](2016-04-09_1541.png) TODO: Cramer Missing Image from Ghost Migration -->

I am not sure why the 0.13.3 files are in the master but if you do not remove these you will get duplicate type errors.
Readme.md can be included if you desire.

Update index.cshtml by changing `Tutorial.jsx` to `Tutorial.js`

Build and Execute.

<!-- ![](2016-04-09_1548.png) TODO: Cramer Missing Image from Ghost Migration -->

Now you have a TypeScript file with a react component.

#### Refactor to utilize TypeScript abilities.
First we will utilize the class feature of TypeScript.  Update the Tutorial.tsx as follows:

```jsx
class CommentBox extends React.Component<{}, {}> {

    render() {
        return (<div className="commentBox">
                Hello, world!I am a CommentBox.
            </div>);
    }
}

ReactDOM.render(
    <CommentBox />,
    document.getElementById('content')
);
```

<!-- ![](2016-04-09_1616.png) TODO: Cramer Missing Image from Ghost Migration -->

#### Now add properties

```jsx
class CommentBox extends React.Component<{aName: string}, {}> {

    render() {
        return (<div className="commentBox">
            Hello, world! {this.props.aName}
            </div>);
    }
}

ReactDOM.render(
    <CommentBox aName="TheFreezeTeam"/>,
    document.getElementById('content')
);
```

<!-- ![](2016-04-09_1611.png) TODO: Cramer Missing Image from Ghost Migration -->

Thanks for the help from:

http://reactjs.net/getting-started/tutorial.html
http://staxmanade.com/2015/08/playing-with-typescript-and-jsx/
http://blog.wolksoftware.com/working-with-react-and-typescript
