#####################################################
ReturnNull.ValueProviders.Web
#####################################################

Description:
============
Testable and extensible wrapper for ModelBinder

Installation Instructions:
===========================
None.

Usage Instructions:
===================
Goals:
------
To allow you to create testable value sources and model builders.

ValueSources: Similar to .NET ValueProviders, value sources will be used
to provide a consistent interface to any data source.
Unlike .NET ValueProviders, it may return multiple values from a single 
source for a single key. If a source supports only one value, it may 
return a list with 1 item.
! ValueSources must return an enumerable object. It must not return null !

ValueSourceProviders: Specifically for model binding, these take the controllerContext
and modelBindingContext parameters from the ModelBinder and allow you to extract data
from those parameters to create your ViewSources.

Setting up:
-----------
When bootsrapping your MVC application, use the static dictionary
[ReturnNull.ValueProviders.Web.ModelBinding.]DataSourceConfig.DataSources to add and remove
sources globally.

Creating your ModelBuilder:
---------------------------
Simply extend [ReturnNull.ValueProviders.Web.]IModelBuilder and implement method BuildModel(..)

Using our ValueProvider:
------------------------
GetValue<T>(string key) : 
	Will get the first value from the first ValueSource to find a value based on the given key. 
	If none found, it will return default(T) (usually null for reference types)
GetValue<T>(string key, T defaultValue) :
	Same as above, but allows you to specify the default value to use if no value was found.
GetValues<T>(string key) :
	Returns all matching values from all value sources.
TryGetValue<T>(string key, out T value) :
	Similar to GetValue but returns false if no values were found. Particularly useful for value types.

Filtering the value sources:
----------------------------
Provide the keys used when registering ValueSources to identify which sources you would like to exclude/limit to/prioritise.
These methods will return a new ValueProvider. The original ValueProvider will remain unaffected.

Excluding(params sources) : Returns a ValueProvider that will not use the specified sources when finding values.
LimitTo(params sources) : Returns a ValueProvider that will only use the specified sources when finding values.
Preferring(params sources) : Returns a ValueProvider that will search the specified sources first when finding values.