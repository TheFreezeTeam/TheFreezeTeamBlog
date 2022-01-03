DocumentName: launch-edge-with-profile
SubFolderName: 2022
Title: Start Edge with a Specific Profile from Powershell 
Published: 01/03/2022
Image: Powershell_256.png
Tags: 
  - PowerShell
  - Edge
  - Start-Process
Author: Steven T. Cramer
Description: How to start Edge using a specific profile from PowerShell?
Excerpt: If the url is opened using the proper profile, I will already be logged in
---

# Start Edge with a Specific Profile from Powershell 

## Scenario

I have a virtual desktop for each project. And I save short cuts to powershell scripts that launch the essential items I need when starting to work on a project.  Which typically consist of opening Windows Terminal to specific directories and opening browser pages to specific resources.  When opening the urls they currently open using the default profile but I want to make the profile explicit.  One key place this comes in handy is the Azure Portal.  

I have profiles configured for each of my Microsoft accounts. If the url is opened using the proper profile, I will already be logged in. 

## Solution

Inside my powershell script I run the following:

```PowerShell
Start-Process -FilePath "C:\Program Files (x86)\Microsoft\Edge Dev\Application\msedge.exe" -ArgumentList "--profile-directory=`"Profile 8`" https://portal.azure.com/"

```

To get the `FilePath` go [edge://version/](edge://version/) and look for `Executable path`.

For `profile-directory` you only need the last part of the `Profile path` from [edge://version/](edge://version/). For my example it is `Profile 8`

Remember to escape you double quotes using the back tic in the `ArgumentList`

## References

https://tahoeninjas.blog/2021/02/06/launching-edge-with-different-profiles-using-shortcuts/
https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/start-process?view=powershell-7.2
