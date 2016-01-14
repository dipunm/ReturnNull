#####################################################
ReturnNull.FunctionalModules
#####################################################

Description:
============
This package provides a framework for defining functions within your application and enforcing that your 
application is properly composed by ensuring each function is represented by a module of your choice.
This package targets .NET 4.5.2 and above.

Installation Instructions:
===========================
In order to use this package, you must define an implementation of IModuleLoader. There will be at least one
NuGet package with an implementation that you may use, however if you cannot find an implementation for your
purpose, you may create your own NuGet package or implement your own within your project.

You will need to define an enum that lists all the functions of your project eg:
enum Functions { Logging, Storage, QueryHandling, Cryptography, EventListeners }

Functions can be infrastructural like logging, but also business functions that could vary between projects.
For example:

One project may be designed to handle trusted data and may require very simple handlers for certain domain events
whereas another project may handle unsafe data and require a different set of handlers, ones that may be more resource intensive.

Another example could be query handlers, where one application must read directly from your data store, and another can read from
a cache or less accurate, but faster data store.

Usage Instructions:
===================
You can compose your modules by calling:

ModuleComposer<TFunction, TModule, TContainer>.Compose(loader, delegate);

where 
	`TFunction` is the enum that defines the functions you modules will define,
	`TModule` is your custom module type,
	`TContainer` is the resulting object that you can use to access your dependencies and start your application,
	`loader` is the custom moduleLoader designed to compose the provided modules into the desired container,
	`delegate` is a method that uses an IModuleDesignator to assign a module to each function.

The moduleDesignator can be used in one of two ways:

designator.Use<MyLoggersModule>(Functions.Logging); //this will expect your module to be newable with a parameterless constructor.
designator.Use(myModule, Functions.Logging); //where myModule is a pre-constructed object of type TModule.
