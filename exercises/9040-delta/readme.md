# Exercise: Finding Delta

## Introduction

A quite common task in data warehousing is to find differences in databases. Often the goal is to transfer only changes from one system to another instead of transferring all data rows every time the target database should be updated.

In this exercise you have to write an algorithm for finding changes (aka delta) in two tab-delimited export files. The original data file is [*data-old.txt*](data-old.txt). The new data file is [*data-new.txt*](data-old.txt). Both files contain data about book sales. The first row contains column headers which are self-explanatory.

## Requirements

* Use .NET 5 and the latest version of C#.
  * Consider using LINQ at least for some aspects of your solution.

* Implement a command-line application that receives two parameters in the command line:
  * Name of the old file
  * Name of the new file

* Compare the new file and the old file based on the following rules:
  * A row is considered as *new* if the combination of *book_iban* plus *book_title* plus *year* is included in the new file but not in the old one.
  * A row is considered as *changed* if the combination of *book_iban* plus *book_title* plus *year* is included in both files, but *genre* or *revenue* differ.
  * A row is considered as *deleted* if the combination of *book_iban* plus *book_title* plus *year* is included in the old file, but not in the new one.

* Print a report about the detected changes on the screen.
  * Data of deleted rows have to be printed with a prefix *-*
  * The *new* content of changed rows have to printed with a prefix *~*
  * New rows have to be printed with a prefix *+*
  * Here is an example for how the report should look like:

```txt
- 426912076-2	Dead Pit, The	Horror	2019	1724
- 283785670-3	Deep in the Woods (Promenons-nous dans les bois)	Horror|Thriller	2019	8193
- 171152819-6	Brainscan	Comedy|Horror|Sci-Fi|Thriller	2019	2387
- 797599742-0	Legacy, The	Horror	2019	8966
- 986735093-6	Daughter of Dr. Jeckyll	Horror	2019	6618

~ 908292251-7	Somewhere Under the Broad Sky	Comedy|Romance	2019	5947
~ 620977674-4	North and South, Book I	Drama|Romance|War	2019	2963
~ 950166360-4	The Gallant Hours	Action|Drama	2019	8178

+ 413090005-6	Kite Runner, The	Drama	2020	1788
+ 934336600-0	Shadowlands	Drama|Romance	2020	1725
+ 992067161-4	Flamenco (de Carlos Saura)	Musical	2020	8431
+ 074908467-7	Spy Kids	Action|Adventure|Children|Comedy	2020	5624
+ 540341412-6	Man from London, The (A Londoni férfi)	Crime|Drama|Mystery	2020	2055
+ 319924564-1	Deadfall	Crime|Drama|Thriller	2020	6496
+ 487723147-1	And Starring Pancho Villa as Himself	Action|Comedy|Drama|War	2020	3270
+ 832348893-2	Phantom Carriage, The (Körkarlen)	Drama	2020	1351
+ 934409029-7	Ghost from the Machine (Phasma Ex Machina)	Sci-Fi	2020	5831
+ 793104345-6	Roula	Drama	2020	2852
+ 362697017-5	Sleep, My Love	Drama|Film-Noir|Mystery	2020	8285
+ 320166792-7	Sketches of Frank Gehry	Documentary	2020	7483
+ 017869267-0	The Flower	Crime	2020	8992
+ 950212264-X	Crazy People	Comedy	2020	4624
+ 877434579-6	Rhinoceros	Comedy|Drama|Fantasy|Mystery|Romance	2020	8639
+ 689957446-2	Zero	Drama	2020	9670
+ 911719189-0	Monuments Men, The	Action|Drama|War	2020	2712
+ 162296451-9	Madeleine	Crime|Drama	2020	8589
+ 028166591-5	High School High	Comedy	2020	6349
+ 784067937-4	20,000 Days on Earth	Documentary|Drama|Musical	2020	7594
+ 902374022-X	Eila, Rampe and Likka	Comedy	2020	7213
+ 147401873-4	Fagbug	Documentary	2020	6189
+ 800625077-4	Hello, Dolly!	Comedy|Musical|Romance	2020	4096
+ 604627971-2	Crows Zero (Kurôzu zero)	Action	2020	5973
```
