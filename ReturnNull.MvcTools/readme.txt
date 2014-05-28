#####################################################
ReturnNull.MvcTools
#####################################################

Description:
============
This package provides a toolset of useful features for use within any ASP.NET MVC project.
This package targets .NET 4.5

Installation Instructions:
===========================
None.

Usage Instructions:
===================

+ HiddenRouteConstraint
-------------------------
You can use the hidden route constraint when setting up your routes:

RouteTable.Routes.MapRoute(
    name: "hiddenRoute1",
    url: "not/to/be/accessed/by/url/{controller}/{action}",
    defaults: new { action = "index" },
    constraints: new
    {
        hidden = new HiddenRouteConstraint()
    }
);