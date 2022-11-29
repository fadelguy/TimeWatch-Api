# TimeWatch-Api

Tired of reporting working hours every day/month? <br>
Now you can automatically report your working hours every day according to your location. <br>
Or you can report for all month (<b>working day's</b>)!

### What's this?
This is a web api project that allow you to punch-in/punch-out/punch-all to https://Timewatch.co.il

### How to use?
This repository publish to https://time-watch.herokuapp.com <br>

#### Api's:

- https://time-watch.herokuapp.com/TimeWatch/punchIn
- https://time-watch.herokuapp.com/TimeWatch/punchOut

You need to **Post** the following data: <br>
``{"employeeId": "XXX", "company": "XXX", "password": "XXX"} ``

##### To report for all month:

- https://time-watch.herokuapp.com/TimeWatch/punchAll

``{"employeeId": "XXX", "company": "XXX", "password": "XXX", "startHour": "09", "endHour": "18", "year": 2022, month: 11} ``

** These parameters are optional and have default value:
* startHour = "09"
* endHour = "18"
* year = current year
* month current month

#### *Security note: this project host on herokuapp, it is EXTREMELY recommended you host your own instance of this API.

![timewatch](https://user-images.githubusercontent.com/32434449/200190918-b88ce304-2939-45f2-a5de-a0108013c9cf.jpg)

## PanchAll

<p align="center">

<img src="https://user-images.githubusercontent.com/32434449/204616974-19e95d3b-dfad-40e6-b94b-99c4f919655c.jpg">
</p>

## IFTTT Example

<p align="center">

<img src="https://user-images.githubusercontent.com/32434449/200191177-d7d3306e-fe6b-4d2f-a5c2-b2512f2b86e2.jpg">
</p>

<p align="center">

  <img src="https://user-images.githubusercontent.com/32434449/200191288-c50a067d-8ad9-45c1-ba3d-68c8aa0df3e8.jpg">
</p>





