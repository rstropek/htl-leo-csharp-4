# User Management Quiz

## Introduction

In this exercise you have to extend a given ASP.NET Core API. The API is about *user and group management*.

The exercise's goal is to practice the following skills that you will need in the upcoming exam:

* Designing web APIs and data models
* Build non-trivial ASP.NET Core API controllers
* Writing non-trivial C# algorithms
* Claims-based authorization
* *Swagger* aka *Open API Specification*

**Note** that this exercise is a bit longer than your exam will be because you have two weeks to complete it. Although your exam will be shorter, the complexity level will be similar.

## Extra Points

* You can earn one extra point for your grade if you finish level 3 (see below for details) of this exercise.
* You can earn a second extra point if you finish level 4.
* Plain copies of the provided final solutions will **not** be accepted.
* If you think you earned extra points, notify me via GitHub issue as usual.

## Starter Solution

This exercise contains a [starter solution](Start). Make yourself familiar with it. The code contains lots of comments. Here are some additional tips:

* The method [`Startup.ConfigureServices`](Start/Startup.cs) contains code that simulates an authenticated user. In real life, authentication would be done through an *OpenID Connect* service like *Azure Active Directory*. We covered that in the course. Here, authentication is simulated and the claims `ClaimTypes.NameIdentifier` and `ClaimTypes.Role` are filled with demo values.

* The starter solution contains all prerequisites for *Entity Framework Core*. It also contains a [`User`](Start/Data/User.cs) class used to store data about users.

* The starter solution contains a helper class [`DemoDataGenerator`](Start/Data/DemoDataGenerator.cs) for filling the DB with demo data. It currently contains code for adding some demo users. You can extend the class so that it also generates records for the DB tables you add to the data context. Note that you can trigger filling the DB by calling the program with the `fill` command-line argument.

## Final Solution

This exercise comes with a [final solution](Solution), too. However, **do not copy all the code from there**. Try to solve the requirements yourself. Use the final solution when you are stuck during self-study time at home or at the end to compare your approach with mine.

## Requirements

### Levels

This exercise consists of multiple levels. If this would be an exam, you would need to solve at least level 1 to get a positive grade. The more levels you master, the better your grade would be (e.g. level 2 to reach up to a 3, level 3 to get a 2, level 4 to get a 1).

## API Urls

You can design the URLs for the requested APIs as you like. However, the [starter solution](Start/requests.http) contains some sample URLs that might help you. Note that you do **not** need to implement the URLs exactly like that. The given URLs are just suggestions.

### Level 1: ASP.NET Core and EFCore Basics

* Extend the EFCore data model so that the program also stores *groups* (i.e. user groups).
  * Each group must have a unique, numeric identifier (i.e. primary key).
  * Each group must have a name (mandatory, max. 100 characters long).
  * Each user can belong to 0..n groups.
  * Each group can have 0..n users as members.
  * Extend [`DemoDataGenerator`](Start/Data/DemoDataGenerator.cs) so that it maintains some meaningful demo groups in the DB.

* Design and build a web API controller for user management that supports the following operations:
  * Get details about the currently signed-in user (see comments about simulated authentication above).
    * The API has to return a JSON object containing the user's ID, name identifier, email, first name, and last name.
  * Get a list of all users.
    * The API has to return a JSON array containing all users. Each user must consist of the user's ID, name identifier, email, first name, and last name.
    * The API has to return an empty array if there are no users in the DB.

* Design and build a web API controller for group management that supports the following operations:
  * Get details about a single group.
    * The caller has to specify the ID of the group to return.
    * The API has to return *not found* if there is no group with the given ID.
    * If a group with the given ID exists, the API has to return a JSON object containing the groups's ID and name.
  * Get a list of all groups.
    * The API has to return a JSON array containing all groups. Each group must consist of the group's ID and name.
    * The API has to return an empty array if there are no groups in the DB.

### Level 2: More Advanced Web APIs

* Extend the web API for returning a list of all users.
  * The API has to accept a query-string parameter `filter`.
  * If given, only user must be returned where the given filter parameter is contained in the user's email **or** first name **or** last name.
  
* Add generation of *Swagger* aka *Open API Specification* to the ASP.NET Core project.
  * Tip: Use the *NSwag.AspNetCore* NuGet package for that.

### Level 3: More Advanced Data Model

* Extend your data model as follows:
  * In addition to all existing properties, each group can have 0..n *child groups* (i.e. recursive data structure).
  * Each group can have 0..1 parent groups.
  * You can assume that there will never be loops in group memberships (e.g. A is member of B, B is member of C, C is member of A). You do not need to handle such cases.

* Extend your web API controller for group management with the following operations:
  * Get all group members of a given group.
    * The caller has to specify the ID of the group to return.
    * The API has to return *not found* if there is no group with the given ID.
    * The API has to return a JSON array containing all groups that are members of the given group. Each group must consist of the group's ID and name.
    * The API has to return an empty array if there are no group members.

### Level 4: Non-Trivial Web API Algorithm

* Extend your web API controller for group management with the following operations:
  * Get all user members of a given group.
    * The caller has to specify the ID of the group to return.
    * The API has to return *not found* if there is no group with the given ID.
    * The API has to return a JSON array containing all users that are members of the given group. Each user must consist of the user's ID, name identifier, email, first name, and last name.
    * The API has to return an empty array if there are no user members.

* Allow the caller to specify a `recursive` parameter for the new web API. If it is `true`, the API does not only return direct members, but also users who are indirect members through nested group memberships.
  * Example:
    * Group *All*
    * Group *Management*, is member of *All*
    * User *Jane* is member of *Management*
    * User *John* is member of *All*
    * API would return only *John* for *All* if `recursive` is `false`.
    * API would return *John* and *Jane* for *All* if `recursive` is `true`.
