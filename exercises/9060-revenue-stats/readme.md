# Order Statistics

## Introduction

You are working in the eBusiness department of a grocery store chain. Your job is to implement a statistical analysis tool for incoming eBusiness orders.

## Technical Requirements

* You **must** use .NET 5.0 to implement this exercise.
* You **must** hand in your work through GitHub.
  * You **should not** check in ZIP files or folders with binaries (e.g. *bin*, *obj*).
* Your code **must** compile without errors and **should** compile without warnings.
* You do **not** need to implement error handling if not explicitly requested.

## Import Interface

Customers deliver orders in the form of text files. [*order-data.txt*](order-data.txt) contains a sample file.

Order files contain *order headers* (start with `ORDER`) and *order details* (start with `DETAIL`). The first two lines in the files contain the column names for orders and order details.

Each order consists of a line with the order header and subsequent order details. Here is an example:

```txt
ORDER	order_id	customer	deliver_to_country
DETAIL	product	amount	unit_price_eur	price_eur
ORDER	1	Gorczany, Olson and Haley	Poland
DETAIL	Oil	6	4	24
DETAIL	Tuna	6	14	84
DETAIL	Tomatoes	8	8	64
ORDER	2	Wiegand, Jenkins and Medhurst	Czech Republic
DETAIL	Cassis	2	9	18
DETAIL	Cheese	8	16	128
DETAIL	Bread	5	12	60
ORDER	3	Davis, White and Halvorson	Sweden
DETAIL	Coffee	2	7	14
DETAIL	Tomato Juice	4	7	28
```

* The order with ID 1 is from a company *Gorczany, Olson and Haley* and it has to be delivered to *Poland*. The order consists of three products:
  * Oil
  * Tuna
  * Tomatoes
* The order with ID 2 is from a company *Wiegand, Jenkins and Medhurst* and it has to be delivered to the *Czech Republic*. The order consists of three products:
  * Cassis
  * Cheese
  * Bread
* The order with ID 3 is from a company *Davis, White and Halvorson* and it has to be delivered to the *Sweden*. The order consists of three products:
  * Coffee
  * Tomato Juice

## Minimum Requirements

You must solve this part of the exam to pass it with a positive grade.

* Attached you find a tab-delimited order file [*order-data.txt*](order-data.txt). You have to read this file into memory to process it accordingly. You can assume that the entire file fits into memory. You do not need to separately read and process each line.

* Calculate and display a summarized revenue statistic containing the revenue (=sum of column `price_eur`) per order ID. Order the result by sum of revenue descending. Here is an example how the output should look like (results fit to data in [*order-data.txt*](order-data.txt)):

```txt
1: 764
10: 722
16: 722
17: 692
11: 635
18: 632
12: 602
13: 557
5: 545
15: 518
14: 508
7: 504
19: 488
9: 487
8: 431
4: 421
2: 402
3: 386
6: 346
20: 272
```

## Additional Requirements

The quality of your code and the extend to which you can solve these requirements influence which positive grade you will get.

* Add a second statistic to your program that displays the revenue  (=sum of column `price_eur`) per customer (=column `customer`). The user should be able to select the statistic she wants using a command-line argument. Here is an example how the output should look like (results fit to data in [*order-data.txt*](order-data.txt)):

```txt
Gorczany, Olson and Haley: 1185
Yundt, Reichert and Ondricka: 1068
Larson-Casper: 1049
Shanahan LLC: 722
Breitenberg-Schmeler: 692
Buckridge, Larkin and Stracke: 635
Ledner and Sons: 632
Lehner-Ziemann: 602
Graham, Kub and Bauch: 557
Boehm, Gleichner and Reilly: 518
Klocko-Waters: 508
Moore Group: 488
Prohaska-Strosin: 487
Jast and Sons: 431
Wiegand, Jenkins and Medhurst: 402
Davis, White and Halvorson: 386
Dicki LLC: 272
```

* Extend the statistic so that it also displays the percentage of the customer's revenue in relation to the total revenue for all customers. Here is an example how the output should look like (results fit to data in [*order-data.txt*](order-data.txt)):

```txt
Gorczany, Olson and Haley: 1185 (11,14 %)
Yundt, Reichert and Ondricka: 1068 (10,04 %)
Larson-Casper: 1049 (9,86 %)
Shanahan LLC: 722 (6,79 %)
Breitenberg-Schmeler: 692 (6,51 %)
Buckridge, Larkin and Stracke: 635 (5,97 %)
Ledner and Sons: 632 (5,94 %)
Lehner-Ziemann: 602 (5,66 %)
Graham, Kub and Bauch: 557 (5,24 %)
Boehm, Gleichner and Reilly: 518 (4,87 %)
Klocko-Waters: 508 (4,78 %)
Moore Group: 488 (4,59 %)
Prohaska-Strosin: 487 (4,58 %)
Jast and Sons: 431 (4,05 %)
Wiegand, Jenkins and Medhurst: 402 (3,78 %)
Davis, White and Halvorson: 386 (3,63 %)
Dicki LLC: 272 (2,56 %)
```
