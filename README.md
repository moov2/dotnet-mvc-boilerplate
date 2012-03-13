# .NET MVC Boilerplate

Project that offers a starting point for ASP.NET MVC projects.

## Getting Started

To get started copy the source from this repository (not including the .git folder) into the directory of the new project. Once all the files have been copied, run the **setup.exe** with the *-r <new-project-name>* argument. An example is shown below.

	.\setup.exe -r MyProject

The command above will setup project directories and files to be inline with the specified solution name, define an available TCP port for the web project and rename the MongoDB connection string to *my-project*. Once this command line process has completed, delete the **setup.exe** file and the project is ready to be worked on.

It is a good idea to run the *assets* target in the **commit.build** build script to generate all the JavaScript & CSS for the project.

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