# Pets

This repository contains my implementation of the 'Pets' programming challenge.

A live version of this web site can be found [here](http://ws-pets.azurewebsites.net/).

## Architecture

This implementation has been written using [ASP.NET Core](https://github.com/aspnet/home) with [.NET Core 2.0](https://github.com/dotnet/core).  The deployment environment for this can be either Windows Server running IIS or Azure App Service.

### Development Environment

The solution will open and run with Visual Studio 2017 providing you have installed the 'ASP.NET and web development' workload during installation.  If not you can update your installation via the Tools -> Get Tools and Features... menu item in Visual Studio.

If you don't have Visual Studio 2017 there is a [free community edition](https://www.visualstudio.com/downloads/).

As with older versions of Visual Studio any missing NuGet packages will be restored when the solution is built.  However in addition to this any missing [Bower](https://bower.io/) packages should be also be restored.  If for any reason this doesn't happen you can force a restore by right clicking on the 'Bower' folder under 'Dependencies' and selecting 'Restore Packages'.

## Requirement Assumptions

There are a some assumptions I've made regarding the requirements which are outlined below:

* The home page of the web site will display "a list of all the cats in alphabetical order under a heading of the gender of their owner".  As this is the main requirement I assumed that it should be instantly visible.
* If an error occurs when retrieving the JSON data I've assumed that this will cause a fatal error which will result in the custom error page being displayed.
* If the JSON returned has missing mandatory fields then I've assumed this is a fatal error which will result in the custom error page being displayed.

## Design Decisions

### Additional 'Owner' Pages

Although it wasn't a requirement I've added an additional page to display a list of owners which links to a page which will display an Owner and their pets details.

I did this to show how the shared Razor Pages can be reused, highlighting why I had separated the functionality.

### Exception Logging

With brevity in mind I decided not to add exception logging to an external source like Event Viewer, Sumo Logic, etc.  Instead I performed the ultimate sin of allowing exception details to be displayed on the Error page.

This is not something I would ever do in any other situation but I wanted to make the errors being raised visible without the overhead of storing, retrieving and displaying them.

### Caching received JSON

I've added caching for the received JSON objects, however this isn't a fully considered framework.  It was added due to the licensing restrictions on the [JSON.NET Schema](https://www.newtonsoft.com/jsonschema) framework which only allows a small number of validations per hour.