# .NET MVC Boilerplate

Ever started a new project and just ended up writing out the same code & copying files from a previous projects? If Yes, and you're developing using the ASP.NET MVC framework then this is for you! .NET MVC Boilerplate offers an extendible & customisable base to give immediate focus on developing the project instead of spending time getting a skeleton in place.

We decided to build this project to give a great starting point instead of having to faff around with repeatable setup.

## Getting Started

Grab the source from this repository by either downloading the zip of the source or cloning via git.

[Download Zip] (https://github.com/moov2/dotnet-mvc-boilerplate/zipball/master)

	cd myproject
	git clone git://github.com/moov2/dotnet-mvc-boilerplate.git
	rm -rf .git

Once the source is on your machine, run the **setup.exe**, you will be prompted to enter a project name. Alternatively you can run **setup.exe** via the command line as shown below.

	setup.exe MyProject

The command above will setup project directories and files to be inline with the specified solution name (value you entered when prompted), define an available TCP port for the web project and rename the MongoDB connection string to *my-project*. Once this command line process has completed, delete the **setup.exe** file and the project is ready to be worked on.

## Example

For an example of the website that the boilerplate offers visit [http://boilerplate.moov2.com] (http://boilerplate.moov2.com). You can login using the credentials *Admin* & *password*.

## Features

* Utilises Simple.Data to enable support for a wide range of database engines.
* Integration with Simple.Data.Mongo for easy connectivity with a Mongo DB datastore.
* User authentication process complete with roles that also includes the ability to create the primary admin user.
* Demonstrates how to secure public methods in Controllers to give access only to authenticated users or restrict to just admins.
* Unit test suite that is also integrated with the build script.
* Provides a simple, customisable & responsive layout that works x-browser & x-device.
* Website implemented using HTML5 semantic mark up.
* CSS normalizations with normalize.css
* CSS written using the Less dynamic stylesheet language providing a wealth of features for improved CSS development.
* Automated build script that: 
	* converts .less files to .css, concats & minifies for a more performant stylesheet.
	* concats custom & third party js files into seperate js files (scripts.js & plugins.js).
	* compiles the solution, failing the build process if there are errors.
	* runs the unit test suite with NUnit outputting results to an .xml file.
	* builds a release version of the website.

## Third Party Libraries

Below is a list of all the third party frameworks & tools used in the boilerplate.

* ASP.NET MVC 3
* jQuery - 1.7.1
* Modernizr - 2.5.3
* NAntContrib - 0.91 RC 1
* lesscss - 1.2.0
* lessc - Feb 15th
* NUnit - 2.6.0.12054
* MVCTurbine with Unity - 3.4
* Simple.Data.Core - 0.14.0.3
* Mongo C# Driver - 1.3.1.4349
* Simple.Data.MongoDB - 0.12.0.1 (Using my personal fork on github https://github.com/peterkeating/Simple.Data.MongoDB)
* DataAnnotationsExtensions.MVC3 - 1.0.1.0
* AutoMoq - 1.6.1
* Moq - 4.0.20926

## TODO

* Implement some JavaScript features just to give an indication of how to add JS into the project.
* Look into possible offering an ASP.NET MVC 4 Beta version to coincide with a future release.
* Document how to switch out the MongoDB adapater for another.
* Document the build script running through how everything is automated.
* Document how the IoC functions.
