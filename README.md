# .NET MVC Boilerplate

Ever started a new project and just ended up writing out the same code & copying files from a previous projects? If Yes, and you're developing using the ASP.NET MVC framework then this is for you! .NET MVC Boilerplate offers an extendible & customisable base to give immediate focus on developing the project instead of spending time getting a skeleton in place.

We decided to build this project to give a great starting point instead of having to faff around with repeatable setup.

## Getting Started

Either download the [zipped source] (https://github.com/moov2/dotnet-mvc-boilerplate/zipball/master), extract and run the setup.exe or, if you want to feel like a pro, from your git bash:

	git clone git://github.com/moov2/dotnet-mvc-boilerplate.git myproject
	rm -rf myproject/.git
	myproject/setup MyProject


Open the Visual Studio .sln file, hit F5 et voila! Take the rest of the day off :)

This will clone the boilerplate code into a directory called "myproject" and then invoke the bundled setup.exe passing it the project name. The command will setup project directories and files to be inline with the specified solution name (value you entered when prompted), define an available TCP port for the web project and rename the MongoDB connection string to *my-project*. Once this command line process has completed, delete the **setup.exe** file and the project is ready to be worked on.

## Updates

### 0.3

* Fixed bug with running multiple sites on the same machine causing clash with cookie name.
* Fixed bug with build script failing when running Java 1.6.
* Removed Roles to simplify switching the method of data storage and have a better starting point that isn't too specific.
* Switched from Less to Sass to reduce friction and preference.
* Updated third party libraries.

### 0.2

* Re-implemented the build process to allow for frictionless development by removing the dependancy on having NAnt.

## Example

For an example of the website that the boilerplate offers visit [http://boilerplate.moov2.com] (http://boilerplate.moov2.com). You can login using the credentials *Admin* & *password*.

## Features

* Utilises Simple.Data to enable support for a wide range of database engines.
* Integration with Simple.Data.Mongo for easy connectivity with a Mongo DB datastore.
* User authentication process that also includes the ability to create the primary user.
* Demonstrates how to secure public methods in Controllers to give access only to authenticated users.
* Unit test suite that is integrated with the build script.
* Provides a simple, customisable & responsive layout that works x-browser & x-device.
* Website implemented using HTML5 semantic mark up.
* CSS resets with normalize.css
* CSS written using the Sass dynamic stylesheet language providing a wealth of features for improved CSS development.
* Automated build script that:
	* minifies and cache busts the CSS.
	* concatenates custom & third party JavaScript files into a single file in order to decrease the number of requests per page.
	* compiles the solution, failing the build process if there are errors.
	* runs the unit test suite with NUnit outputting results to an .xml file.
	* builds a production ready version of the website.

## Third Party Libraries

Below is a list of all the third party frameworks & tools used in the boilerplate.

* ASP.NET MVC 3
* jQuery - 1.8.3
* Modernizr - 2.6.2
* NAntContrib - 0.91 RC 1
* NUnit - 2.6.0.12054
* MVCTurbine with Unity - 3.4
* Simple.Data.Core - 0.14.0.3
* Mongo C# Driver - 1.4.2.4500
* Simple.Data.MongoDB - 0.12.0.1
* DataAnnotationsExtensions.MVC3 - 1.1.0.0
* AutoMoq - 1.6.1
* Moq - 4.0.20926

## TODO

* Implement some JavaScript features just to give an indication of how to add JS into the project.
* Look into possible offering an ASP.NET MVC 4 Beta version to coincide with a future release.
* Document how to switch out the MongoDB adapater for another.
* Document how the IoC functions.
