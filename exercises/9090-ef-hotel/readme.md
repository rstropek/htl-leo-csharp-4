# *Hotel* Exercise

## Introduction

You job is to implement the data layer for an online hotel portal.

If you solve all requirements, notify me via GitHub issue. You will get **between 1 and 2 extra points** depending on the quality of your code.

## Data Model

Here are the requirements for the data model of the hotel system:

* The system can store hotels. Hotels have the following properties:
  * Name
  * Address (Street, ZIP Code, City)

* The system can store hotel specials. The following specials have to be available:
  * Spa
  * Sauna
  * Dog friendly
  * Indoor pool
  * Outdoor pool
  * Bike rental
  * eCar charging station
  * Vegetarian cuisine
  * Organic food

* Each hotel can have 0..n specials.

* Each hotel can offer 0..n different room types. Note that room types are hotel-specific. Every hotel has its unique room types. Room types have the following properties:
  * Reference to the hotel offering the room type
  * Titel (e.g. *Luxury single room with a view to the alps*)
  * Description (e.g. *Single room with balcony. Enjoy a great view to the surrounding moutains.*)
  * Size (e.g. *12m²*)
  * Flag indicating whether the room is disability-accessible (yes/no)
  * Number of rooms of that type available in the corresponding hotel

* The system can store the price for each hotel room. Prices change over time, so each price record has the following properties:
  * Reference to hotel room type
  * Valid from date (can be empty)
  * Valid until date (can be empty)
  * Price in EUR per room per night

Create a data model covering these requirements. You can add technical details (e.g. primary keys) where necessary. Make assumptions regarding data type details (e.g. field lengths).

Create an Entity Framework data context and use it to generate the data model in a SQL Server database.

## Adding Data

Implement a console app with which a user can fill the database with the following demo hotels:

* Pension Marianne
  * Am Hausberg 17, 1234 Irgendwo
  * Dog friendly, organic food
  * Room types:
    * 3 x 10 m² single room for 40€
    * 10 x 15 m² double room for 60€
    * No rooms with disability access
* Grand Hotel Goldener Hirsch
  * Im stillen Tal 42, 4711 Schönberg
  * Spa, sauna, indoor pool, outdoor pool
  * Room types:
    * 10 x 15 m² single room for 70€
    * 25 x 30 m² double room for 120€
    * 5 x 45 m² junior suites for 190€
    * 1 x 100 m² honeymoon suite for 300€
    * All rooms with disability access

## Querying Data

For displaying the hotels on a website, you have to create a console app to export hotel data in the markdown format. Here is an example output:

```md
# Pension Marianne

## Location

Am Hausberg 17
1234 Irgendwo

## Specials

* Dog friendly
* Organic food

## Room Types

| Room Type   |  Size | Price Valid From | Price Valid To | Price in € |
| ----------- | ----: | ---------------- | -------------- | ---------: |
| Single room | 10 m² |                  |                |       40 € |
| Double room | 15 m² |                  |                |       60 € |

# Grand Hotel Goldener Hirsch

## Location

Im stillen Tal 42
4711 Schönberg

## Specials

* Spa
* Sauna
* Indoor pool
* Outdoor pool

## Room Types

... (table similar to the one shown above)

```
