using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Web.Mvc;

namespace ReturnNull.UmbracoMvcCatalyst
{
    /// <summary>
    /// An empty surface controller to ensure Umbraco sets up a 
    /// SurfaceRouteHandler object. Umbraco will use reflection and
    /// find this class and register it. This controller has no actions
    /// and should not be used in any way within your client application
    /// </summary>
    public class NullSurfaceController : SurfaceController
    {
    }
}
