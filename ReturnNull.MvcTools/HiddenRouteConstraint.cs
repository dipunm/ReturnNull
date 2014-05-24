using System.Web;
using System.Web.Routing;

namespace ReturnNull.UmbracoApi
{
    /// <summary>
    /// Ensures that the route using this constraint cannot be matched by the
    /// request Url.
    ///
    /// This is useful if you like having very specific routes set up, but you
    /// need that generic route just for child actions.
    /// </summary>
    public class HiddenRouteConstraint : IRouteConstraint
    {
        public bool IsRecursing(HttpContextBase httpContext)
        {
            var inUse = httpContext.Items["NonMatchingRoute"] as bool?;
            return inUse != null;
        }

        public void EndRecursionCheck(HttpContextBase httpContext)
        {
            httpContext.Items["NonMatchingRoute"] = null;
        }

        public void BeginRecursionCheck(HttpContextBase httpContext)
        {
            httpContext.Items["NonMatchingRoute"] = (bool?)true;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (IsRecursing(httpContext))
                return true; // pretend I am not here.

            try
            {
                BeginRecursionCheck(httpContext);

                var routeData = route.GetRouteData(httpContext);
                return
                    routeData == null ||
                    routeData.Values["controller"] == null ||
                    routeData.Values["action"] == null ||
                    routeData.Values["controller"].ToString() != values["controller"].ToString() ||
                    routeData.Values["action"].ToString() != values["action"].ToString();
            }
            finally
            {
                EndRecursionCheck(httpContext);
            }
        }
    }
}