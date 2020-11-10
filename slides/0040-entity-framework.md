# Entity Framework

O/RM in .NET

---

## What is EF?

* Object-relational mapper (O/RM)
* Supports many different DB providers ([list](https://docs.microsoft.com/en-us/ef/core/providers/index))<br/>
  examples:
  * MS SQL Server
  * SQLite
  * PostgreSQL
  * MySQL
  * In-Memory (for testing)
* Latest version: *EF Core 3.0*
* NuGet (example): [*Microsoft.EntityFrameworkCore.SqlServer*](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/)

---

## Getting Started

* Follow [*installing* docs](https://docs.microsoft.com/en-us/ef/core/get-started/) to add EF to project
  * Make sure to install global EF tools: `dotnet tool install --global dotnet-ef`
* Work through [tutorials](https://docs.microsoft.com/en-us/ef/core/get-started/) in EF docs
  * [Tutorial for ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)
* Tips:
  * Run local [SQL Server using Docker](https://hub.docker.com/_/microsoft-mssql-server):<br/>
    `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server`
  * Install and use free [SQL Server Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server  Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
  * Use [In-Memory DB](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) for simple test scenarios

---

## Building a Model

```cs
<!--#include file="exercises/0010-intro/Person.cs" -->
```

* Read more about [creating a Model](https://docs.microsoft.com/en-us/ef/core/modeling/)

---

## Setting up the Context

```cs
<!--#include file="exercises/0010-intro/Context.cs" -->
```

---

## Writing Data

```cs
<!--#include file="exercises/0010-intro/WriteToDB.cs" -->
```

* Read more about [writing data](https://docs.microsoft.com/en-us/ef/core/saving/)

---

## Writing Data to Related Sets

```cs
<!--#include file="exercises/0030-joins/AddingWithJoin.cs" -->
```

* Note *single* call to `Add` and `SaveChangesAsync`

---

## Querying Data

```cs
<!--#include file="entity-framework/0010-intro/ReadFromDB.cs" -->
```

* Read more about [querying data](https://docs.microsoft.com/en-us/ef/core/querying/)

---

## Querying Data

```cs
<!--#include file="entity-framework/0030-joins/QueryWithJoin.cs" -->
```

* `Include`: Getting `Order` with related `Customer`

---

## EF Core + ASP.NET Core Cheat Sheet

[Entity Framework Cheat Sheet](ef-aspnet-cheat-sheet.md)

---

## Further Readings and Exercises

* Readings
  * [Entity Framework Documentation](https://docs.microsoft.com/en-us/ef/#pivot=efcore)
  * [EF Tutorial with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro)
* Videos
  * Want to know more details? Watch [Entity Framework Core](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Entity-Framework-Core) on *Channel 9*
