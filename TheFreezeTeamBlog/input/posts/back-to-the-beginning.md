Title: Back to the Beginning
Tags: 
  - Java
Author: Steven T. Cramer
Image: backToBeginning.png

---
I have started a new front-end project, and lo and behold, the project is written in Java with JSP files!
I chuckle to myself because Java was the first language I learned way back in 1995, and after a short score as a chef , I'm working with it again! Full circle. 
 No big deal from a coding standpoint as the front end is still rendered using HTML/CSS. However the environment in which the coding happens isn't the same as my previous projects which were primarily React and React Native.  I decided it would be fun to detail the setup process which was not nearly as painful as I was expecting. 
The computer I'm working on is a Windows 10 Pro, V. 1709,  OS Build 16299.967. 


What I needed to build and run the Java app came down to three things. Java JRE and JDK, Apache Tomcat and Apache Netbeans. 
The Java run-time Environment  (JRE) and  the Java Development Kit (JDK) is essentially Java itself and chances are you have it installed already. 
 NetBeans is a Java IDE, It's used primarily to write and build Java projects. When you build a  Java web application project in NetBeans its produces a  WAR file (Web application ARchive). I sort of think of it similar to an APK in the Android world, in that it is the entire application compressed into a single file. How accurate this line of thinking is, I'm not sure, so you may not want to take it to heart.  
Apache Tomcat is a server application that unpacks the war file into a functional application that is then served via local server.
The process ends up being: 


*  Open the project in NetBeans
*  Build it
*  Move the resulting WAR file from the NetBeans `/target` directory to the `/webapp` directory in Tomcat
*  In Tomcat's `/bin` directory click on the `startup.bat` file

How cool is that? I hope this provides a little guidance. Thanks for reading!

