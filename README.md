# Dispenser Services
Services for scheduling medication printing and acts as a gRPC client

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

4. Select Web.SchedulerService for the Startup Project

5. Rebuild the project

6. Press the f5 to run the project