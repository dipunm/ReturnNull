#####################################################
ReturnNull.FunctionalModules.AutofacLoader
#####################################################

Description:
============
Provides an Autofac implementation for IModuleLoader.

Installation Instructions:
===========================
None.

Usage Instructions:
===================
When using ModuleComposer:
 - use Autofac.Module as TModule
 - use Autofac.IContainer as TContainer
 - use your own enum for TFunction

using Autofac;
ModuleComposer<MyFunctions, Module, IContainer>.Compose(...);

This will allow you to use native Autofac modules. All Autofac module features should work as expected.