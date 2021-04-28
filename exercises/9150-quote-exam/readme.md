# *Favorite Quotes* Quiz

## Introduction

You have read the book *Känguruchroniken* and you think that it would be great to have a website where people can exchange funny (wrong) quotes like the ones [the Känguru is citing in the book](https://die-kaenguru-chroniken.fandom.com/wiki/Falsch_zugeordnete_Zitate). Here are some examples:

* *Frage nicht, was dein Land für dich tun kann, frage was du für dein Land tun kannst.* - Kim Jong-il
* *Dies ist mein Leib, der für euch hingegeben wird.* - Gina Wild
* *Da hat das rote Pferd sich einfach umgekehrt und hat mit seinem Schwanz die Fliege abgewehrt.* - Johann Wolfgang von Goethe
* *Wenn man ein 0:2 kassiert, dann ist ein 1:1 nicht mehr möglich.* - Satz des Pythagoras

You call your project *quotexchange*. Your job is to build a web API that can act as the backend for *quotexchange*.

## Starter Solution

This exam contains a [starter solution](Start). Make yourself familiar with it. The code contains lots of comments. Here are some additional tips:

* The starter solution is very similar to the one you got during your last exercise.

* In real life, authentication would be done through an *OpenID Connect* service like *Azure Active Directory*. We covered that in the course. Authentication has been simplified for this exam and **all the necessary setup code is already in the starter solution**. The following table contains three ready-made tokens (can also be found in [*requests.http*](requests.http)). They fill the claim `ClaimTypes.NameIdentifier` with demo values that fit to the demo data generator (see below).

| Name Identifier Claim | Token |
|-----------------------|-------|
| foo.bar@acme.corp     | `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmb28uYmFyQGFjbWUuY29ycCIsIm5hbWUiOiJiYXIiLCJnaXZlbl9uYW1lIjoiZm9vIiwibmJmIjoxNjE3NzgwNDYwLCJleHAiOjE2MTgzODUyNjAsImlhdCI6MTYxNzc4MDQ2MCwiaXNzIjoiaHR0cHM6Ly91aS5xdW90ZXhjaGFuZ2UuY29tIiwiYXVkIjoiaHR0cHM6Ly9hcGkucXVvdGV4Y2hhbmdlLmNvbSJ9._EYIOexObS5MQYE2WGol_XjSrRgwZvsjKj6MqOqxaaw` |
| john.doe@acme.corp    | `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJqb2huLmRvZUBhY21lLmNvcnAiLCJuYW1lIjoiZG9lIiwiZ2l2ZW5fbmFtZSI6ImpvaG4iLCJuYmYiOjE2MTc3ODA1MDcsImV4cCI6MTYxODM4NTMwNywiaWF0IjoxNjE3NzgwNTA3LCJpc3MiOiJodHRwczovL3VpLnF1b3RleGNoYW5nZS5jb20iLCJhdWQiOiJodHRwczovL2FwaS5xdW90ZXhjaGFuZ2UuY29tIn0.v05zA-topRo2j-T5i9qZmFwHIduQJZsS_HNQUcFcxhk` |
| jane.doe@acme.corp    | `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJqYW5lLmRvZUBhY21lLmNvcnAiLCJuYW1lIjoiZG9lIiwiZ2l2ZW5fbmFtZSI6ImphbmUiLCJuYmYiOjE2MTc3ODA1MzcsImV4cCI6MTYxODM4NTMzNywiaWF0IjoxNjE3NzgwNTM3LCJpc3MiOiJodHRwczovL3VpLnF1b3RleGNoYW5nZS5jb20iLCJhdWQiOiJodHRwczovL2FwaS5xdW90ZXhjaGFuZ2UuY29tIn0.rwK5J_DcfVmZwEruxaewMawukZVPzMsqVlqURcFi7sQ` |

* The starter solution contains all prerequisites for *Entity Framework Core*. It also contains a [`User`](Start/Data/User.cs) class used to store data about users.

* The starter solution contains a helper class [`DemoDataGenerator`](Start/Data/DemoDataGenerator.cs) for filling the DB with demo data. It currently contains code for adding some demo users (fit to the tokens mentioned above). If you want, you can extend the class so that it also generates records for the DB tables you add to the data context. Note that you can trigger filling the DB by calling the program with the `fill` command-line argument.

## Requirements

Extend the given starter solution so that your program fulfills the following requirements. The details about the requested web API have been specified in an [*Open API Specification* (Swagger) file](https://app.swaggerhub.com/apis/rstropek/quotexchange/1.0). **Make sure that your API fulfills the requirements stated there** (incl. URL structure, HTTP status codes, data structures, etc.).

## Minimum Requirements

You have to fulfill the following requirements for a positive grade.

* Extend the EFCore data model so that the program also stores *quotes*. See schema `Quote` in the [*Open API Specification* (Swagger) file](https://app.swaggerhub.com/apis/rstropek/quotexchange/1.0) for details.
  * Remember to store the user who created each quote (*creator*).

* Extend the EFCore data model so that the program also stores *votes* for *quotes*.
  * For every *quote*, there can be 0..n *votes*.
  * A vote is either an *upvote* (+1) or a *downvote* (-1).
  * Because of privacy reasons, do **not** store which user voted.

* Extend the [`DemoDataGenerator`](Start/Data/DemoDataGenerator.cs) so that it generates some demo quotes.

* Design and build a web API controller for *quote* management. It has to support the following operations (for details see [*Open API Specification* (Swagger) file](https://app.swaggerhub.com/apis/rstropek/quotexchange/1.0)):
  * Clear (=delete all quotes and the associated votes) (`POST /api/quotes/clear`).
  * Get my quotes (`GET /api/quotes/my`).
  * Get all quotes (`GET /api/quotes`). Note that this is the **only** API method that can be called anonymously.

### Further Requirements

The number of fulfilled requirements and the quality of the related code influence what grade you will get. For detailed, technical requirements see [*Open API Specification* (Swagger) file](https://app.swaggerhub.com/apis/rstropek/quotexchange/1.0).

* Implement the web API method *Add quote* (`POST /api/quotes`).

* Implement the web API method *Vote* (`POST /quotes/{id}/vote`).

* Change the web API method *Get all quotes* (`GET /api/quotes`) so that votes are returned raked by popularity descending. Popularity is calculated by summing up all upvotes (+1) and downvotes (-1) for each quote.

## Self Evaluation

The file [*requests.http*](requests.http) contains sample requests with expected results. You can use those sample requests to test your API. **Tip**: Use VSCode's [*REST Client* extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) to run the requests. Alternatively, you can copy the requests to e.g. *Postman*.
