# Order Import Quiz

## Introduction

In this exercise, you have to implement a command-line tool for importing order information.

## Functional Requirements

### Data Model

```txt
+----------------------------------------------+
| Customer                                     |
|----------------------------------------------|
| Id (int, PK)                                 |
| Name (string, max. 100 chars)                |
| CreditLimit (decimal, length 8, precision 2) |
+----------------------------------------------+
                       1
                       |
                       n
+----------------------------------------------+
| Order                                        |
|----------------------------------------------|
| Id (int, PK)                                 |
| CustomerId (int, FK to Customer)             |
| OrderDate (date + time)                      |
| OrderValue (decimal, length 8, precision 2)  |
+----------------------------------------------+
```

You can earn an **extra point** for your grade if you add a *unique index* on `Customer.Name` (see [docs](https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/data-annotations#index) for details).

### General Requirements

Implement a command-line tool that supports four operations:

* *Import*: Import two files containing data about customers and their orders (see also data model above). You program has to add all rows from the files to the database.

* *Clean*: Delete all rows from all database tables.

* *Check*: Print a list of customers on the screen who have exceeded their credit limit.

* *Full*: Execute *Clean*, then *Import*, then *Check*.

### Import

* Example for the *import* command: `dotnet run -- import customers.txt orders.txt`
  * First parameter indicates that an import should be done.
  * Second parameter is the file name with customer data.
  * Third parameter is the file name with order data.

* Example customer import file:

```txt
CustomerName	CreditLimit
Foo	1000
Bar	2000
Baz	3000
```

* Example order import file:

```txt
CustomerName	OrderDate	OrderValue
Foo	2020-01-01T08:30:00	500
Foo	2020-01-02T09:30:00	500
Bar	2020-01-03T10:30:00	1000
Bar	2020-01-04T11:30:00	2000
Baz	2020-01-05T12:30:00	1500
Baz	2020-01-06T13:30:00	1000
```

### Clean

* Example for the *clean* command: `dotnet run -- clean`

### Check

* Example for the *check* command: `dotnet run -- check`

* The program has to print a list of customer names for which the sum of all their order values (`Order.OrderValue`) is greater than their credit limit (`Customer.CreditLimit`).

### Full

* Example for the *full* command (file names in analogy to *import* command): `dotnet run -- full customers.txt orders.txt`

## Non-Functional Requirements

* Use .NET 5 and C# 9

* Use Entity Framework Core to store data in a database

* *SQL Server* is recommended. As an alternative, you can use *PostgreSQL* or *Sqlite*.
