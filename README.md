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