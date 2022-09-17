---
DocumentName: powershell-script-to-configure-git-repo-branches
Title: Web Development Branching Strategy
Published: 01/09/2017
Tags: 
  - C# 

Author: Steven T. Cramer
Description: Go to your VSTS site and create new project or repo.  From the Code tab, you should see something like below that displays the URL to your repository.
Excerpt: Go to your VSTS site and create new project or repo.  From the Code tab, you should see something like below that displays the URL to your repository.
---

# Create Repo in VSTS

Go to your VSTS site and create new project or repo.
From the Code tab, you should see something like below that displays the URL to your repository.

![](2017-01-09_1115.png)


# Branch per Environment

When developing code I prefer to have a branch that corresponds to each server environment that we have.
Typically there is a `Production` environment, a `QA` environment, and a `Development` environment.

Each developer has their own branch or branches but they integrate via pull requests into the `Sprint` branch.

The branches flow as pictured

![](2017-01-09_1111.png)

To automate the creation of the base branches I wrote a simple PowerShell script:

```Powershell
function InitializeGitRep ($repoUrl)
{
    git clone $repoUrl
    cd $repoUrl.Split('/')[-1]

    New-Item ReadMe.md -type file
    git add ReadMe.md
    git commit -m "Add ReadMe"

    git checkout -b Sprint master
    git push -u origin Sprint
    git branch -d master
    git branch Production
    git branch QA
    git branch Development    
    git push origin --all
    git remote show origin
}
```

At this point, you will need to manually set your branch policies to require pull requests and code reviews.
Microsoft has an API in preview https://www.visualstudio.com/en-us/docs/integrate/api/policy/configurations but this is
beyond the scope of this post.
Not to mention my allotted time.
