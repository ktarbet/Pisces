Linux
[![Pisces Build](https://www.travis-ci.com/usbr/Pisces.svg?branch=master)](https://www.travis-ci.com/github/usbr/Pisces)
Windows
[![Build status](https://ci.appveyor.com/api/projects/status/vrtk5m141gfrb6gt?svg=true)](https://ci.appveyor.com/project/ktarbet/pisces)

Pisces   
======

Pisces is a time series database including a desktop application that graphs and analyzes time series data. Pisces is designed to organize, graph, and analyze natural resource data that varies with time: gauge height, river flow, water temperature, etc. 

![image](https://github.com/user-attachments/assets/e69c47e1-f6dc-4a0f-9408-0c62a5bc658e)


 
The Pisces time series database is designed to be fast and simple.  The default database engine is Sqlite http://www.sqlite.com/
However, Pisces also supports postgresql, MySql, SqlServer, and ~~SqlCompact~~.

The key programs and assemblies  (HydrometServer.exe, Reclamation.Core.dll and Reclamation.TimeSeries.dll) work under Windows or Linux/mono.  
 
Hydrologist, Engineers (especially modelers), and Programmers have used these Pisces libraries to manage large amounts of time series data with ease. The main component in the library called Series can be used without any database if desired.

Pisces has many server-based features. For example, Pisces running on a server can send text messages or make phone calls, to a list of people until someone acknowledges, when changing conditions merit that. [Alarm Description](https://github.com/usbr/Pisces/wiki/alarm-description)

Download the Pisces user manual here: [User Manual](Doc/Pisces_User_Manual.doc)  
Download a summary bulletin here: [Summary Bulletin](Doc/Pisces_bulletin.DOC)

## History and Motivation

Pisces initially started so users could manage large amounts of environmental time series data from sensors or modeling outputs.   Using Pisces different data sets (scenarios) are easy to compare to each other including when different statistics such as exceedance as selected.


The ability to write simple to understand time series equations is one motivation to create Pisces.

Example simple equation involving three different time series and two different time steps.
```
(pal_af-pal_af[t-1])/1.98347+(jck_af[t-1]-jck_af[t-2])/1.98347+heii_qd
```

The Legacy system requires writing the equation above like this:

```
CL
CREATE JCK AF
CREATE PAL AF
CREATE HEII QD
CREATE HEII QU
G JCK/AF
G PAL/AF
MATH
LINE1*1 +1
MS=TOTAL
MC=AF
LINE1+LINE2
E 2
E 1
SP=LINE1
MS=TOTAL
MC=CS
LINE1-LINE2 +1
LINE3/1.98347
E 2
 
G HEII /QD
MATH
MS=HEII
MC=TQU
LINE2+LINE3
MS=SPAC
MC=AF
SP=LINE1
1484450-LINE5
 
G HEII/QU
MATH
RANGE=OCT03,SEP30
MS=HEII
MC=QU
LINE6=LINE4
MARK HEII QU
 
REPLACE
SHOW TOTAL,SPAC,HEII /QD,QU,AF
```


Another example of equation that is not user friendly.

```
 CASE  WHEN <<input2>> > .001 THEN (<<input1>>gsc(1264,385,<<tsbt>>)+gsc(1264,386,<<tsbt>>) + gsc(1264,393,<<tsbt>>)) * ( gsc(1264,388,<<tsbt>>)*power(<<input2>>,2) - gsc(1264,387,<<tsbt>>)<<input2>> + gsc(1264,389,<<tsbt>>) ) ELSE 0 END
 ```


## Boise State University - Computer Science Student Contributors

Isaac Zurn, Soraya Yazdanpour, James Norwood, Sam McMahon, Rob Herrera, Ashley Gertman, Tara Felzien, Tyler Egan, Mack Bryan, Nate Brinton


