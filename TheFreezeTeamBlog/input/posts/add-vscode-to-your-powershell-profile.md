Title: Add VSCode to your PowerShell profile path
Tags: 
  - C#
Author: Steven T. Cramer

---
Open PowerShell.

To edit your profile in notepad type:

`notepad $profile`

If your PowerShell profile doesn't exist:

`New-Item -Type File -Force $profile`

Inside the profile add

`$env:Path += ";C:\Program Files (x86)\Microsoft VS Code"`

Save and restart PowerShell.

`code $profile` should open your profile using VSCode

`code .` should now open the current directory in VSCode





