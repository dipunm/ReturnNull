using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ReturnNull.UmbracoMvcCatalyst.MvcComponents
{
    /// <summary>
    /// An alternative HijackingController base class with no direct dependencies
    /// to the UmbracoContext.
    /// </summary>
    public abstract class HijackingController : Controller, IRenderMvcController
    {
        /// <summary>
        /// Default controller, in constructor action invoker is set to default Index action
        /// </summary>
        protected HijackingController()
        {
            ActionInvoker = new RenderActionInvoker();
        }

        public abstract ActionResult Index(RenderModel model);
    }
}
