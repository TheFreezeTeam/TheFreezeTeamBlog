Title: Integration Testing with Fixie
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

Draft:  Started with simple quick start

During and interview coding challenge a couple months ago I was directed to a website called [HackerRank](https://www.hackerrank.com).  Hackerrank has a beginners challenge called "30 Days of programming".  I am using the Day 0 Hello World application here as the code to be tested.

Given the interview process, seems to involve a ton of Console app challenges, I thought I would use this as another quick start **Default Convention** example of [Fixie](http://fixie.github.io/docs/quick-start/)

# The code:
This is the Day0 code we are going to test:

```csharp

namespace Day0
{
  using System;

  class Program
  {
    static void Main(string[] args)
    {
      // Declare a variable named 'inputString' to hold our input.
      String inputString;

      // Read a full line of input from stdin (cin) and save it to our variable, input_string.
      inputString = Console.ReadLine();

      // Print a string literal saying "Hello, World." to stdout using cout.
      Console.WriteLine("Hello, World.");

      // TODO: Write a line of code here that prints the contents of input_string to stdout.
      Console.WriteLine(inputString);
    }
  }
}

```

#Step-by-step:

Create new Class Library named Day0Tests 
*File->New->Project*

Add Nuget packages from Nuget Package Manager Console. 
*Tools->Nuget Package Manager->Nuget Package Manager Console*

```
PM> Install-Package Fixie
...
PM> Install-Package Shouldly
...
```

Create tests for sample class:


```csharp


```

Build.

Test->Windows->Test Explorer

Find the Calculator Tests and Execute.


---

Respawn
StructureMap

Integration tests that use database:
Database

Respwan
https://lostechies.com/jimmybogard/2015/02/19/reliable-database-tests-with-respawn/

Respwan intelligently deletes all table data from the database. Unless that table is added to the ignored list.
https://github.com/jbogard/respawn


So if you have some read-only data needed for your tests  that you don't want to initialize, add those tables to the ignore list.

> "I ignore specific tables as part of my checkpoint, but those are read-only tables in which I don't add or delete data in any of my tests."

To get list of all tables

```SQL
SELECT * FROM information_schema.tables
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME
```

Terms

Test Class - a class containing test methods.
Test Fixture - Instance of a Test Class.
Test Method - A method containing test code to be executed.
Test Case - Single execution of a test method.
