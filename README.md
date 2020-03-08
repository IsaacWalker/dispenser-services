# Dispenser Services
Services for scheduling medication printing

### Setup
#### Windows 10
1. Install Visual Studio 2019 Community Edition with:
    * Asp.Net and web development
    * .Net desktop development
    * Data storage and processing
    * .Net core cross-platform development

2. Install IIS Manager (Only for Hosting on IIS)
    1. Press Windows + S, search for "Turn  Windows features on or off"
    2. Enable "Internet Information Services"
    3. Press Windows + S, "Windows Administrative Tools" and select IIS Manager

3. Open the .sln in Visual Studio

4. In Solution Explorer, Right click Solution 'DispenserService' and go to properties.

5. Select Multiple startup projects and enable Web.SchedulerService and Web.Frontend

6. Exit the dialog, right click libman.json under Web.Frontend and restore client-side dependencies.

7. Rebuild the project

8. Press the f5 to run the project

### Overview

#### Physical Diagram

The system will physically manifest itself in a number of components:

1. The Dispenser/ Printer is a 3D printer with a Raspberry Pi attachted. This Pi runs
a Flask web server that can receive requests about printing ODF's.

2. The Scheduler Web Service is a remote on-premise service that runs a scheduler capable of printing the ODF's a patient 
requires in for on time of their usage. It sends commands to the Dispenser/Printer to print ODFs.

3. The Nurse App is a Web Application that is used by the nurse to interact with the system, such as viewing the patients 
profile, administered and pending medications, and confirm the administration of a medication.

4. The Database which stores patient info, prescription info and odf info.

![alt text](https://github.com/IsaacWalker/dispenser-services/blob/master/imgs/physical.png "Physical Diagram")

#### Architecture Diagram

The architecture consists of three services:

1.  The scheduler service, which provides the Scheduler, an API for the Dispenser to update ODF info and an API
to provide views to either native or web frontends. Its an ASP.Net Core MVC Web App

2. The Frontend service which is the Web UI for the app that is displayed for the nurses device (in this case a tablet).
Its an ASP.Net Core MVC Web App using bootstrap and JQuery

3. The Dispenser Service, which is a REST Api for interacting with the printer and dispenser. Its a python flask web service.

4. An Entity Framework Core InMemory Database which represents an SQL Database that would be used in production. 

![alt text](https://github.com/IsaacWalker/dispenser-services/blob/master/imgs/architecture.png "Architecture Diagram")

#### Interaction diagram

This shows the interaction of the Scheduler service with the Dispenser in order to print an ODF.

![alt text](https://github.com/IsaacWalker/dispenser-services/blob/master/imgs/flowchart.png "Interaction Diagram")


