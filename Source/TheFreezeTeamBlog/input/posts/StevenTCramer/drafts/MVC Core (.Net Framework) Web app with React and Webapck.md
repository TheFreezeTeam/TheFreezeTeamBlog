Title: MVC Core (.Net Framework) Web app with React and Webapck
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

This blog is based heavily on:
https://www.typescriptlang.org/docs/handbook/react-&-webpack.html

We assume that you’re already using Node.js with npm.

This will get us started with an MVC Core full .net Framework (Windows Only) Application.  With the full framework, we can use Entity Framework 6.

In visual studio Create new project <ProjectName>
Choose ASP.NET Core Web Application (.NET Framework)
![](/content/images/2016/09/2016-09-25_1151.png)

Choose Web Application
![](/content/images/2016/09/2016-09-25_1158.png)

Build and run Application.

![](/content/images/2016/09/2016-09-25_1210.png)

Add `bundles` folder:



To start, we’re going to structure our project in the following way:

<ProjectName>/
   +- App/
   |
   +- Build/
TypeScript files will start out in your `App` folder, run through the TypeScript compiler, then webpack, and end up in a bundle.js file in Build.

Let’s scaffold this out:
