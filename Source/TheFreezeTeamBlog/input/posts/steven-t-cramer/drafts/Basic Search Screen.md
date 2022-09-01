Title: Basic Search Screen
Tags: 
  - CSharp 
  - Blazor 
  - dotnetcore 
  - Blazor-State
Author: Steven T. Cramer
Excerpt: ReduxDevTools off by default. 
Published: 03/12/2099
---

Given a collection of items in the domain we want to search for a subset of them based on some criteria.

Example:
Given a set of People we want to search by Manager.

The domain would have a Query that takes the ManagerID
The domain QueryResult would return a collection of Person.

The PageGet Query would be blank

UI:

View
ViewModel
PageGet
PagePost



Domain:
Query
Command

Separation of concerns

ViewModel has properties for various purposes, 
Options from server
Selections from client
Results from Server Projected as DTOs

In theory these do not have to be the same viewmodel.

