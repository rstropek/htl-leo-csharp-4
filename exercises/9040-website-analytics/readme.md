# Website Access Statistic Quiz

## Introduction

In this quiz you have to analyze log data from a fictitious website.

## Technical Requirements

* You **must** use .NET 5.0 to implement this exercise.
* You **must** hand in your work through GitHub.
  * You **should not** check in ZIP files or folders with binaries (e.g. *bin*, *obj*).
* Your code **must** compile without errors and **should** compile without warnings.

## Minimum Requirements

Note: In an exam, you would have to solve this part of the quiz to pass the exam with a positive grade.

* Attached you find a tab-delimited webserver log file [*access-log.txt*](access-log.txt). You have to read this file into memory to process it accordingly. You can assume that the entire file fits into memory. You do not need to separately read and process each line.

* Calculate and display a summarized access statistic containing the number of downloads per image URL per month (YYYY-MM). For each image URL, you have to display the total number of downloads. Here is an example how the output should look like:

```txt
pic001.jpg:
        2020-01: 18
        2020-02: 13
        2020-03: 13
        ...
        TOTAL: 172
pic002.jpg:
        2020-01: 20
        2020-02: 15
        2020-03: 28
        ...
        TOTAL: 177
pic003.jpg:
        2020-01: 20
        2020-02: 26
        2020-03: 15
        ...
        TOTAL: 180
pic004.jpg:
        2020-01: 21
        ...
...
```

* The attached file [*monthly-result.txt*](monthly-result.txt) contains correct results. You can check your solution, it should produce the same numbers.

## Additional Requirements

Note: The quality of your code and the extend to which you can solve these requirements influence which positive grade you would get in a real exam.

**Homework:** If you check in a working solution **until end of this week**, you will get one extra point for your grade.

* Split your solution into two different projects:
  * *LogAnalysis*: Command-line app. It must *only contain input-output-related code* (e.g. reading file, printing result on the screen, handling command-line arguments, etc.). For processing, this app has to use the following class library.
  * *LogAnalysis.Logic*: Class library. It must contain *all processing logic* (e.g. parsing of strings, LINQ statements, etc.).

* Change your command-line app so that the previously created, monthly download statistic is generated if the user passes the parameter *monthly* in the command-line.

```txt
C:\LogAnalysis\LogAnalysis>dotnet run -- monthly
pic001.jpg:
        2020-01: 18
        2020-02: 13
        2020-03: 13
        ...
        TOTAL: 172
pic002.jpg:
        2020-01: 20
        ...
...
```

* If the users passes *hourly* in the command-line, calculate and display a summarized access statistic containing the percentage of downloads per image URL and hour (100% = total number of downloads of that image). Here is an example how the output should look like:

```txt
C:\LogAnalysis\LogAnalysis>dotnet run -- hourly
pic001.jpg:
        00:00: 4,65 %
        01:00: 1,74 %
        02:00: 6,40 %
        ...
        22:00: 4,07 %
        23:00: 4,07 %
pic002.jpg:
        00:00: 4,52 %
        01:00: 3,95 %
        02:00: 6,21 %
        ...
        22:00: 2,26 %
        23:00: 8,47 %
...
```

* The attached file [*hourly-result.txt*](hourly-result.txt) contains correct results. You can check your solution, it should produce the same numbers.

* Extend your program so that the user can *optionally* pass an image URL filter as the second parameter in the command line.

```txt
C:\LogAnalysis\LogAnalysis>dotnet run -- monthly pic002.jpg
pic002.jpg:
        2020-01: 20
        2020-02: 15
        ...
        TOTAL: 177

C:\LogAnalysis\LogAnalysis>dotnet run -- hourly pic002.jpg
pic002.jpg:
        00:00: 4,52 %
        01:00: 3,95 %
        02:00: 6,21 %
        ...
        22:00: 2,26 %
        23:00: 8,47 %
```

## Advanced Requirements

Note: If you would aim for 100% in your exam, you have to be able to solve these requirements, too.

**Homework:** If you check in a working solution that includes these requirements **until end of this week**, you will get one additional extra point for your grade.

* If the users passes *photographers* in the command-line, calculate and display a summarized download statistics for each photographer ordered by number of downloads descending. Here is an example how the output should look like:

```txt
C:\LogAnalysis\LogAnalysis>dotnet run -- photographers
Kenan Acevedo: 190
Bhavik Brookes: 185
Fabio Millington: 182
Zahara Ireland: 182
Fabio Millington: 182
Bhavik Brookes: 180
Fabio Millington: 177
Bhavik Brookes: 176
Fabio Millington: 176
Kenan Acevedo: 172
Jolie Molina: 152
Daniel Slater: 146
Cinar Gardner: 144
Cinar Gardner: 132
Jolie Molina: 124
```

* In order to find out which photographer took which picture, you have to read [*photographers.json*](photographers.json) and combine it appropriately to the data from [*access-log.txt*](access-log.txt).
