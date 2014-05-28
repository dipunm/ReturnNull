#####################################################
ReturnNull.UmbracoApi
#####################################################

Description:
============
Provides a wrapper around the Umbraco API where testability was compromised.

Installation Instructions:
===========================
This package assumes that you have an IoC container installed, however, it does not enforce
any one IoC container. The IoC container MUST however be registered within the MVC DependencyResolver.

It is recommended that you bind these implementations in transient scope:
IPublishedContentStore => new PublishedContentStore(UmbracoContext.Current.ContentCache)
IPublishedMediaStore => new PublishedMediaStore(UmbracoContext.Current.MediaCache)
IUmbracoContext => new PseudoUmbracoContext(UmbracoContext.Current)

Usage Instructions:
===================
IPublishedContentStore: A wrapper around the ContextualPublishedContentCache.
IPublishedMediaStore: A wrapper around the ContextualPublishedMediaCache.
IUmbracoContext: A wrapper around a subsection of the UmbracoContext.

By utilising these interfaces and using dependency injection, mocking these dependencies will become easy 
and unit testing will be feasible.