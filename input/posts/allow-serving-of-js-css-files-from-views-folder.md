Title: Allow serving of js, ts, css and map files from Views folder.
Published: 04/28/2016
Tags: 
  - TypeScript 
  - MVC5 
  - Javascript 
Author: Steven T. Cramer
Description: In a traditional MVC 5 application structure you can not access css or js files located in the view directory.
Excerpt: In a traditional MVC 5 application structure you can not access css or js files located in the view directory.
---

In a traditional MVC 5 application structure you can not access css or js files located in the view directory.

If you would like to move the associated css and js files into the view folder you need to update the web.config located in the Views folder.

![](/content/images/2016/04/ProjectStructure.png)

Default settings are:
```
  <system.webServer>
    <handlers>
      <remove name="BlockViewHandler"/>
      <add name="BlockViewHandler" path="*" verb="*" preCondition="integratedMode" type="System.Web.HttpNotFoundHandler" />
    </handlers>
  </system.webServer>
```

To:
```
  <system.webServer>
    <handlers>
      <remove name="BlockViewHandler" />
      <add name="JavaScript" path="*.js" verb="GET,HEAD" type="System.Web.StaticFileHandler" />
      <add name="CSS" path="*.css" verb="GET,HEAD" type="System.Web.StaticFileHandler" />
      <add name="TypeScipt" path="*.ts" verb="GET,HEAD" type="System.Web.StaticFileHandler" />
      <add name="Map" path="*.map" verb="GET,HEAD" type="System.Web.StaticFileHandler" />              
      <add name="TypeSciptTsx" path="*.tsx" verb="GET,HEAD" type="System.Web.StaticFileHandler" />
      <add name="BlockViewHandler" path="*" verb="*" preCondition="integratedMode" type="System.Web.HttpNotFoundHandler" />
    </handlers>
  </system.webServer>
```

Now you can add a link to javascript file 

```
<script src="~/Views/Home/Index.js"></script>
```

*Note that typescript files are only design-time files they will be compiled to .js files so dragging a typescript file into a cshtml page will create the appropriate .js link.*

**Although I recommend switching to JimmyMVC style applications.  This can be handy in projects that are not of the JimmyMVC structure.

