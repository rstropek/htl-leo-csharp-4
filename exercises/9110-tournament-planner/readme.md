# Tournament Planner

## Introduction

You are volunteering in a local tennis club. Luckily, your club has **exactly 32 active members**. In the upcoming week, the yearly club championship will take place. It is your job to implement the software to organize the matches of the championship.

In each match, two random club members (=players) play against each other. The winner reaches the next round. This will be repeated until two players reach the final. Here is a table displaying the number of matches per round:

| Round | Matches | Total players |
| ----- | ------: | ------------: |
| 1     |      16 |            32 |
| 2     |       8 |            16 |
| 3     |       4 |             8 |
| 4     |       2 |             4 |
| 5     |       1 |             2 |

## Data Model

Your software must store data about *players* and *matches* in a relational database. Consider the following requirements when designing your database model:

* You have to store *players* (=club members participating in the championship). For each player, you have to store the following properties:
  * Name (mandatory)
  * Phone number (text, optional)
* You have to store *matches*. For each match, you have to store the following properties:
  * Round number (mandatory; values between 1 and 5, see also table above)
  * References to both players participating in the match (mandatory)
  * Winner of the match (optional; will be set once the match is completed)
* Every record in every table has to have an auto-generated numeric primary key (type `int`)

## Starter Code

To make your life easier, you get a [starter solution](starter) with existing code. You only have to fill in the blanks. The starter solution also contains lots of automated tests. Currently, nearly all of them fail. If you want to have all points, all tests must become green.

Take some time to make yourself familiar with the starter solution. It is part of the exercise to learn how to read and extend existing code. Note: You **must not** alter the code for the automated tests.

## Minimum Requirements

Your code **must** compile without errors.

You must solve at least the following requirements for a positive grade:

* Complete the data model classes so that they can fulfill the data model requirements stated above.
  * You can make meaningful assumptions for technical details that have not been defined in the requirements.
* Extend the EFCore database context class so that you can generate EFCore migrations and create a database.
* Add *at least* the following helper methods for the following tasks to the EFCore database context class. Stubs for the methods are already in the EFCore database context class `TournamentPlannerDbContext`. You just have to add the implementation.
  * Add a club member (=player)
  * Add a match between two given club members
  * Set the winner of an existing match
  * Get a list of all matches that do not have a winner yet
  * Delete everything (matches, players); this operation will be used during testing and after the championship to delete all personally identifiable information (*personenbezogene Daten*)
* Add *at least* the following web API endpoint:
  * Get a list of all players (*GET* to */api/players*).

## Additional Requirements

You grade will depend on the completeness and quality of the implementation of the following requirements:

### General Requirement

Use **async methods** for database access wherever technically possible.

### Filtered Players

Add a helper method to the EFCore database context class for getting a list of all club members (=players). The caller can optionally provide a name filter. Return only those members whose name contains the given filter. A stub for the helper method already exists in the EFCore database context class `TournamentPlannerDbContext`. You just have to add the implementation.

### Generate Match Records

Add a helper method to the EFCore database context class for generating match records for the next round. For that, the following rules apply:

* If there are *any* matches in the DB that do *not* have a winner, throw an exception.
* If the number of players in the DB is not 32, throw an exception.
* Count the number of matches to find out the next round.
  * If you find no matches in the DB, the next round is the first round.
  * If you find 16 completed matches, the next round will be the second round.
  * If you find 24 completed matches, the next round will be the third round.
  * If you find 28 completed matches, the next round will be the fourth round.
  * If you find 30 completed matches, the next round will be the fifth round (=final).
  * If there is any other number of matches in the DB, throw an exception.
* Generate match records for the next round.
  * If you generate the first round, generate matches between random players.
  * If you generate any other round, generate matches between random winners of the previous round.

A stub for the helper method already exists in the EFCore database context class `TournamentPlannerDbContext`. You just have to add the implementation.

## Transactions

* All database operations of the following functions have to be done within a DB transaction:
  * Delete everything
  * Generate match records

### ASP.NET Web API

Add web API endpoints for the following operations. They have to use the methods of the EFCore database context.

* Add a player (*POST* to */api/players*)
* Get a list of all players (*GET* to */api/players*). The caller can optionally provide a name filter (e.g. */api/players?name=Foo*). Return only those members whose name contains the given filter.
* Get a list of all matches that do not have a winner yet (*GET* to */api/matches/open*).
* Generate match records for next round (*POST* to */api/matches/generate*).
