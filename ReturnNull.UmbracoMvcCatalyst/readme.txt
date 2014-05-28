#####################################################
ReturnNull.UmbracoMvcCatalyst
#####################################################

Description:
============
Provides some helpers and components designed to enable better integration between
Umbraco and MVC.

Installation Instructions:
===========================
None.

Usage Instructions:
===================
Goals:
- Enable the use of standard MVC Controllers instead of Surface controllers.
You should call:
Catalyst.RegisterControllers(Assembly.GetExecutingAssembly())
From within your Global.asax within the OnApplicationStarted method.

- Similarly, remove the dependency on RenderMvcController (mainly to remove the dependency on UmbracoContext)
If you require the use of a hijacking controller, you may extend the HijackingController instead of RenderMvcController.

- Allow layout files to be shared across both Umbraco pages and MVC pages.
You should call:
Catalyst.PrepareFilter(GlobalFilters.Filters);
From within your Global.asax within the OnApplicationStarted method.
You will also need to ensure that your layouts do not inherit UmbracoTemplatePage but instead inherit UmbracoMvcTemplatePage.
You will lose access to the UmbracoHelper and other features that will fail to function properly without a fully initialised UmbracoContext object.
You will gain access to a Content property which will represent the CurrentPage.
In the case that you are loading an MVC page, the CurrentPage will represent a fake UmbracoPage whose properties are sourced from ViewData.
If you must identify between a real IPublishedContent vs an MvcContent object, you may use the extension method: IsMvcContent()