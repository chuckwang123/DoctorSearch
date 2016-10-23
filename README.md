#Doctor Search
A site contains a listing of doctors. Users can browse for doctors given a specific specialty, area, review score etc.

Write a class which when given a doctor, provides a list of similar doctors, in a prioritized order. You define what similar means and the result ordering, but clearly document any assumptions in your code.

Please, include unit tests.You can assume the entire directory of doctors fits into memory, and write in whatever language you are most comfortable with. 

##Frameworks and Libraries
API:

1, .net Framework

2, Newton.Json

3, FuzzyString

4, ASPNet.API

5, Dapper

6, Angular 2

7, Typescript

Unit Test:

1, Moq

2, FluentAssertion

HomePage:

1, bootstrap table

2, mustache

3, Jquery

4, bootstrap-table-filter-control

Database:

1, SQL Server (RDS)

##Defination
Priority:

Name -> specialties -> Age -> Score -> Area


Similar:

Name :  Use Dice coefficient, consider value < 0.5 means similar.

specialties: If post doctor has specialties, the output must be contain at least one of specialties.

Age	:  Use Age Range, implement in Javascript.

Sex	:  Exact Match, implement in Javascript.

ReviewScore :  Exact Match, implement in Javascript.

AreaCode : Arround the area(10,25,50 miles), need Latitude and longtitude, or third-part APIs.



##Implementation
The code has been seperete in 2 parts

1, API Endpoints:

Get /api/doctors

Post /api/doctors/search with body(Doctor)


a. Get /api/doctors:

Return all of doctors

b. Post /api/doctors/search

Pass Doctor Sample as parameter.

rule 1, if find the exact matched doctor,add to output

rule 2, if find the similar name doctors,add to output

rule 3, if Doctor Sample include specialties, filter by the specialties

rule 4, if Doctor Sample include specialties and no matched doctors before. return all doctors who can filter by the specialties


2, Page:
a, age can be select by number range

b, Sex can be select by column header drop down

c, Score can be select by column header drop down

d, Search bar can search all content

e, page can fresh use fresh button

f, every column can be sorted


##Data Format
Use SQL Server(AWS - RDS) to get data, read as JSON

| Doctor                       |
|------------------------------|
| int - Id                     |
| varchar - Name               |  
| Date - DOB			       |
| Bool - Sex			       |
| Varchar - Telephone	       |
| Varchar - AreaCode	       |
| int  - reviewScore           |    
| Name - string            	   |
| specialties - List<specialty>|


| specialty          |
|--------------------|
| Id - int           |
| Name - Vachar      |


| DoctorspecialtyRelation|
|------------------------|
| Id - int               |
| DoctorID - Int      	 |
| specialtyID - Int      |


##future plan
1, implement area code

2, implement age, score, sex in the endpoint

3, change filter column value which is number to number range

4, javascript unit test(Jasmine)

