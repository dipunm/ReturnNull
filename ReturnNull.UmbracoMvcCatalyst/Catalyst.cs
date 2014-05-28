using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using ReturnNull.UmbracoApi;
using ReturnNull.UmbracoMvcCatalyst.MvcComponents;
using umbraco;
using Umbraco.Core;

namespace ReturnNull.UmbracoMvcCatalyst
{
    /// <summary>
    /// The catalyst provides some convenience methods to help initialise your application
    /// to allow Umbraco and MVC to work better together.
    /// </summary>
    public static class Catalyst
    {
        public static void PrepareFilter(GlobalFilterCollection filters)
        {
            filters.Add(new UmbracoMvcCatalystAttribute());
        }


        /// <summary>
        /// By passing in an assembly, this method will search for all of your controllers and route them
        /// safely such that the routes will never conflict with Umbraco pages. These routes ensure that 
        /// you can access your controller actions as child actions. If you wish to access your routes by
        /// Url, you should add extra routes the traditional way.
        /// </summary>
        /// <remarks>
        /// We recommend using AttributeRouting.
        /// </remarks>
        public static void RegisterControllers(Assembly assembly)
        {
            foreach (var controllerType in assembly.GetTypes().Where(t => t.Inherits<ControllerBase>()))
            {
                var controllerName = controllerType.Name;
                if (!controllerName.EndsWith("Controller", StringComparison.Ordinal))
                    continue;

                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);

                var surfaceRouteHandler = RouteTable.Routes.OfType<Route>()
                    .First(r =>
                        r.DataTokens != null &&
                        r.DataTokens.ContainsKey("umbraco") && 
                        r.DataTokens["umbraco"].ToString() == "surface"
                    )
                    .RouteHandler;

                RouteTable.Routes.MapRoute(
                    name: "thesearenotthedroidsyouarelookingfor_" + controllerName,
                    url: "thesearenotthe/" + controllerName + "/or/{action}/youarelookingfor",
                    defaults: new { controller = controllerName },
                    constraints: new
                    {
                        nonMatching = new HiddenRouteConstraint()
                    }
                    ).RouteHandler = surfaceRouteHandler;
            }
        }
    }
}
