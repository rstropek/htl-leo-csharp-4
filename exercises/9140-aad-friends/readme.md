# Friends in AAD

## Prerequisites

**Note** that for this homework you must have attended the lectures regarding *OpenID Connect* (OIDC), *Azure Active Directory* (AAD), and *ASP.NET Core Authentication and Authorization*. If you haven't or if you couldn't follow along the demos I showed there, please recap the topics relevant for you by watching the lecture videos:

* [Part 1: OpenID Connect fundamentals, registering apps in AAD](https://youtu.be/ND6EKbGb7vQ)
* [Part 2: Angular Client, Graph API, custom API protection](https://youtu.be/R_LIjafo6cY)
* [Part 3: Testing with Postman, accessing claims in C#](https://youtu.be/Ca52hqMiNro)

## Introduction

In this homework, you have to extend the sample that we created together during our school lessons. Your job is to build a *Friends Directory* based on our school's AAD.

You can get up to **two extra points** for this homework. **Everybody** must hand in the best try. If you think you covered all requirements, let me know via GitHub issue. I will check your code and - if everything works as expected - assign you the extra points.

## Functional Requirements

* As a user, I want to login using my AAD credentials.

* As a user, I want to logout if I no longer need the application.

* As a user, I want to search for other people in AAD using their names (as shown in the lecture, part 2)

* As a user, I want to be able to mark my friends (=users in AAD). The application has to store all my selected friends.

* As a user, I want to see all my friends including their names whenever I login.

* As a user, I want to be able to delete a person from my friend list.

## Non-Functional Requirements

* Store the friends list in SQL Server. Access the database using *Entity Framework Core*.
  * Do **not** use names in your database to reference AAD users. Always use their ID.

* Build an API with *ASP.NET Core* with which a *Single Page App* can list friends, add friends, and remove friends for an authenticated user.
  * Anonymous users (unauthenticated) **must not** be able to call the API successfully. The API **must** return *Unauthorized* in that case.

* Use *Angular* to create the user interface.

## Tips

You can use the sample that we created during the lecture as the basis for this homework. You do not need to register a new API and a new client app in AAD. Just reuse the one you created during our lecture. You do not need to create a new Angular client app. Use the one we created in the lecture. For ASP.NET Core, you can decide on your own whether you want to start from scratch or build on what you already have.
