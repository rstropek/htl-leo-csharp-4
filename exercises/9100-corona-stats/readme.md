# Corona Statistics

## Introduction

The Austrian government publishes data about Covid-19 cases daily. You would like to import this data in a database and make it easily accessible through a web API. Additionally, you would like to publish a chart that everybody can take a look at.

Everybody has to do this homework and hand in his best try. **You can earn up to two extra points for this homework**:

* You get one extra point for a working Angular app that accesses the API and display a tabular result.

* You get a second point if you display the result in a chart, too.

## Technical Requirements

* Use .NET 5 and C# 9
* Build an ASP.NET Core 5 API
* Use Entity Framework Core 5 for accessing the database
* Use Angular 11 for creating the web front end

## Functional Requirements

### Data Model

#### Federal State (*Bundesland*)

| Properties/Relations | Remarks                                           |
| -------------------- | ------------------------------------------------- |
| ID                   | Primary key of federal state                      |
| Name                 | Name of the federal state (e.g. *Oberösterreich*) |
| Districts            | All districts in the federal state (see below)    |

#### District (*Bezirk*)

| Property       | Remarks                                                                    |
| -------------- | -------------------------------------------------------------------------- |
| ID             | Primary key of district                                                    |
| State          | Reference to federal state to which the district is related to (see above) |
| Code           | Official district code                                                     |
| Name           | District name                                                              |
| Covid-19 Cases | Data about Covid-19 cases in the district (see below)                      |

Notes:

* Official district codes (e.g. 410 for *Linz-Land*) can be read from [*Statistik Austria*](https://www.statistik.at/web_de/klassifikationen/regionale_gliederungen/politische_bezirke/index.html) (column *Kennziffer pol. Bezirk*)
* We do not differentiate between the different districts of Vienna. Vienna is just one record with *900* as its code.

#### Covid-19 Cases

| Property        | Remarks                                                                |
| --------------- | ---------------------------------------------------------------------- |
| ID              | Primary key of the data record                                         |
| Date            | Date of the import                                                     |
| District        | Reference to district to which the case data is related to (see above) |
| Population      | Population of the district                                             |
| Cases           | Number of Covid-19 cases                                               |
| Deaths          | Covid-19 related deaths                                                |
| 7-day incidence | 7-day incidence                                                        |

### Data Import

* The user can trigger a data import by sending a *POST* request to the URL */api/importData*.

* If the database does not contain *federal states* and *districts*, import this data from [*Statistik Austria*](https://www.statistik.at/web_de/klassifikationen/regionale_gliederungen/politische_bezirke/index.html) ([URL for CSV file](http://www.statistik.at/verzeichnis/reglisten/polbezirke.csv)) before importing any other data.

* Use [Covid-19 data from *data.gv.at*](https://www.data.gv.at/katalog/dataset/2f6649b6-2b2d-49a9-ab31-6c7e43728001) as your data source. The URL to download the CSV file is [https://covid19-dashboard.ages.at/data/CovidFaelle_GKZ.csv](https://covid19-dashboard.ages.at/data/CovidFaelle_GKZ.csv).
  * If the database already contains data for the current day, delete the data of the current day before importing data.
  * Correlate data from *Statistik Austria* with data from *data.gv.at* using the district code (*GKZ* in Covid-19-data and *Kennziffer pol. Bezirk* in data from *Statistik Austria*).

* The API has to return the HTTP status code...
  * ...[*Ok*](https://http.cat/200) (200) if import was successful
  * ...[*Internal Server Error*](https://http.cat/500) (500) in case of an error

### Master Data

* The user can get a list of all federal states by sending a *GET* request to the URL */api/states*.

* The JSON result has to contain the states and all their districts. Here is an example how the result could look like:

```json
[
    {
        "id": 1,
        "name": "Oberösterreich",
        "districts": [
            {
                "id": 1,
                "code": 410,
                "name": "Linz-Land"
            },
            { ... },
            ...
        ]
    },
    { ... },
    ...
]
```

### Cases per State

* The user can get the history of Covid-19 cases by sending a *GET* reqest to the URL */api/states/&lt;state-id&gt;/cases* (*state-id* is the ID of the federal state).

* The JSON result has to contain all the Covid-19 case history for the selected state. Here is an example how the result could look like:

```json
[
    {
        "id": 1,
        "date": "2020-11-01",
        "district": {
            "id": 1,
            "code": "...",
            "name": "..."
        },
        "population": ...,
        "cases": ...,
        "deaths": ...,
        "sevenDaysIncidents": ...
    },
    { ... },
    ...
]
```

### Total Cases

* The user can get the history of total Covid-19 cases by sending a *GET* request to the URL */api/cases*.

* The JSON result has to contain the sum of all Covid-19 cases for all districts. Here is an example how the result could look like:

```json
[
    {
        "id": 1,
        "date": "2020-11-01",
        "populationSum": ...,
        "casesSum": ...,
        "deathsSum": ...,
        "sevenDaysIncidentsSum": ...
    },
    { ... },
    ...
]
```

### User Interface

* Create an Angular app that accesses the API described above.

* After starting the Angular app, the user should see a table with the total case history (see *Total Cases* API above).

* The user can optionally select a district (see *Master Data* API above). If she does, the result table has to contain Covid-19 data for the selected state (see *Cases per State* API above).

## Technical Tips

* Use C#'s `HttpClient` to read CSV files from the internet. Make sure to read [the related documentation page](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests) for using `HttpClient` in ASP.NET Core (the [*Basic usage* pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests#basic-usage) is sufficient for this homework).

* Consider [*ng2-charts*](https://github.com/valor-software/ng2-charts) for charts in Angular.

* Embed your Angular app using one of the following methods:
  * [Enable CORS](https://docs.microsoft.com/en-us/aspnet/core/security/cors) if you want to run Angular and your Web API on different ports on *localhost*.
  * [Serve static files in web root](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files) and copy your Angular app's build result into the web root folder.
  * Include a [Single Page app (SPA)](https://github.com/softawaregmbh/consulting-netcore-microservices-sample/blob/504fab7e4199b16a84129539463f9a1b506c5022/NetCoreMicroserviceSample/NetCoreMicroserviceSample.Api/Startup.cs#L208) proxy in your ASP.NET Core project so you access your Angular app indirectly through the ASP.NET Core app (recommended approach for this homework).
