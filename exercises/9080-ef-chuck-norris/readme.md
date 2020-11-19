# Entity Framework *Chuck Norris* Exercise

## Introduction

[Chuck Norris](https://en.wikipedia.org/wiki/Chuck_Norris#Trivia) is an interesting person. The internet is full of ["facts"](https://en.wikipedia.org/wiki/Chuck_Norris_facts) about him. For instance, it is said that people wanted to add Chuck Norris to [Mount Rushmore](https://en.wikipedia.org/wiki/Mount_Rushmore), but the granite was not tough enough for his beard. Chuck Norris can also divide by zero and he can order a Big Mac at Burger King and get one.

Your job is to create a database with Chuck Norris "facts" and fill it from the [chucknorris.io web API](https://api.chucknorris.io/).

## Database structure

Our sample database consists of a single table:

| Column        | Nullable | Data Type                | Comment                       |
| ------------- | -------- | ------------------------ | ----------------------------- |
| Id            | No       | Integer                  | Auto-generated primary key    |
| ChuckNorrisId | No       | String, max. length 40   | *id* from *chucknorris.io*    |
| Url           | No       | String, max. length 1024 | *url* from *chucknorris.io*   |
| Joke          | No       | String, no max length    | *value* from *chucknorris.io* |

## Functional Requirements

* Create a console app.
  * The console app receives the number of jokes that should be imported in the command line.
  * If no argument is given, use 5 as the default value.
  * The maximum number of jokes to be imported is 10.
  * Print a meaningful error message if the user specifies a wrong parameter (e.g. a value greater than 10).
  * If the user specified *clear* as the command line argument, you have to delete all data from the database using the following SQL statement: `DELETE from <your-table-name-here>`.

* Import random jokes from [https://api.chucknorris.io/jokes/random](https://api.chucknorris.io/jokes/random).
  * Jokes from the category *explicit* must not be imported.

* Store jokes in the database.
  * Do not add jokes multiple times to the database (check it using the *id* from *chucknorris.io*). If the web API gives you a joke that you already have, try a new random joke. Retry getting a new joke 10 times. If you have all these jokes already in the database, we assume that we have all the jokes that exist. In that case, print a status message and exit.
  * If an error happens during an import, the program should print an error message and exit. In that case **no data must be imported** (i.e. use a transaction).

## Non-functional Requirements

* Use .NET 5 and C# 9

* Use Entity Framework Core to store data in a database

* *SQL Server* is recommended. As an alternative, you can use *PostgreSQL* or *Sqlite*.

## Technical Tips

* Use `HttpClient` to call the web API (see [docs](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0#examples) for sample code)
  
* Use `JsonSerializer` to read the JSON response into .NET objects (see [docs](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0#how-to-read-json-into-net-objects-deserialize) for sample code)
