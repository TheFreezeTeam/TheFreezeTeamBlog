Title: Code Search that will give you superpowers
Published: 07/10/2019
Image: noimage.jpg
Tags:
  - DeveloperTools
  - CodeSearch
Author: Steven T. Cramer
Excerpt: The Freeze Team is disseminating the Code Search Superpower. Come see how you too can wield it!
Description: The Freeze Team is disseminating the Code Search Superpower. Come see how you too can wield it!
---

![][SourcegraphImage]

Scenarios where superpowers would be handy!

> "Welcome to Acme Corp's software development team!  For your first task, we would like you to fix the bug that keeps causing the "Sorry something broke" message that about 100 users reported last week. We are not sure which of our 12 repositories contains the source of the problem. Good luck!"

> "Version 2 of SuperCoolPackage is out and fixes a security vulnerability that we need to update right away. Please find all references and update them. Also apply the migration steps as required."

> "Hmmm, I know what I need to do. I did something similar before. Where is that code?"

> I am using this open source AcmeWidget library and wonder, "How are they implementing this method?"

People have written millions of lines of code over the years. And stored it in, who knows how many, repositories.

>As of May 2019, GitHub reports having over 37 million users and more than 100 million repositories (including at least 28 million public repositories), making it the largest host of source code in the world. [Wikipedia](https://en.wikipedia.org/wiki/GitHub)

What if I were to give you **The Freeze Team superpower** to search code across all the repositories and a booster that lets you "find references" and "go to definition." All from within your browser?  

Today is your lucky day!
Kevin Dietz, a fellow Freeze Team member, discovered and shared this power earlier this week.
And I have used it everyday since.

So what is the name of this Superpower?

<a href="https://sourcegraph.com"><img alt="Sourcegraph" src="https://storage.googleapis.com/sourcegraph-assets/sourcegraph-logo.png" height="32px" /></a>

Take a few minutes right now to check it out.
Let me help guide you to the path of **higher productivity**.
Oh btw did I tell you this superpower is **free** up to 20 users?

## Installation

Let's start with the [Quickstart guide](https://docs.sourcegraph.com/)

Sourcegraph is deployed as a docker container and thus you need to have [docker](https://www.docker.com/) another amazing tool installed.  Go ahead take a few minutes to go install docker.

**...**

Now that you have docker installed you can fire up the Sourcegraph container with the following docker command for your operating system:

### Linux or Mac:

```powershell
docker run --publish 7080:7080 --publish 2633:2633 --rm --volume ~/.sourcegraph/config:/etc/sourcegraph --volume ~/.sourcegraph/data:/var/opt/sourcegraph sourcegraph/server:3.5.1
```

### Windows

Currently there is an issue with using volumes from Windows.
The following command will run and store the configuration inside the container.
The container will be restarted when docker restarts.

```powershell
docker run --restart always --publish 7080:7080 --publish 2633:2633 sourcegraph/server:3.5.1
```

> **Coming soon** [Docker will run on Windows Subsystem for Linux 2](https://engineering.docker.com/2019/06/docker-hearts-wsl-2/) and these problems will go away.

I recommend making a copy of your configurations, as updating the container will lose your config until the volume issue is resolved.

## Configuration

Now that you have the container running navigate to http://localhost:7080/

You will need to create an account and sign in.  After you get signed in, you can start adding repositories for Sourcegraph to index.
Go to http://localhost:7080/site-admin and select `External services`

I added my public GitHub repositories and some I reference often very quickly with the following config:

```json
{
  "url": "https://github.com",

  "token": "<Enter your access token here>",
  "repositoryQuery": [
    "none"
  ],
  "orgs": [
    "TimeWarpEngineering",
    "TheFreezeTeam"
  ],
  "repos": [
    "AnthemGold/MyVault-web",
    "HERCone/herc-in-blazorstate",
    "aspnet/AspNetCore",
    "dotnet/command-line-api",
    "dotnet-architecture/eShopOnWeb",
    "dotnet-architecture/eShopOnContainers",
    "jbogard/MediatR",
    "jbogard/ContosoUniversityDotNetCore-Pages",
    "jbogard/ContosoUniversityDotNetCore"
  ],
}
```

I also added some private repos from Azure DevOps.

```json
{
  "url": "https://StevenTCramer:<Enter your access token here>@timewarpenterprises.visualstudio.com",
  "repos": [
    "/XXX/_git/XXX"
  ]
}
```

## Usage

Click on the Sourcegraph logo which will take you to http://localhost:7080/search and enjoy the new superpower. With a little practice you can wield this investigative superpower with ease.

<!---
## Mentorship

If you are tired of coding alone and would like someone on your side come join us at the [FreezeTeam](https://twitter.com/TheFreezeTeam1).

Please see [YouTube Video] for examples of searches.
-->

[SourcegraphImage]: /../images/Sourcegraph.png "Sourcegraph"
