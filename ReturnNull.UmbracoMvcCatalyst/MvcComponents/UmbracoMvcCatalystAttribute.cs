using System.Web.Mvc;
using ReturnNull.UmbracoApi.Wrappers;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace ReturnNull.UmbracoMvcCatalyst.MvcComponents
{
    /// <summary>
    /// This attribute will allow the UmbracoMvcTemplatePage to provide an implementation
    /// of the IPublishedContent object whose properties are fed by ViewData. Allowing you
    /// to use the same cshtml layouts across Umbraco and MVC pages.
    /// </summary>
    public sealed class UmbracoMvcCatalystAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            if (filterContext.HttpContext.Items[Constants.TemplateContentKey] is IPublishedContent)
                return; //no need to run unnecessarily.

            var contentId = UmbracoContext.Current.PageId;
            IPublishedContent content;
            if (contentId == null)
            {
                content = new MvcContent(filterContext.Controller.ControllerContext);
            }
            else
            {
                var contentStore = DependencyResolver.Current
                    .GetService(typeof (IPublishedContentStore)) 
                        as IPublishedContentStore;

                content = contentStore != null ? 
                    contentStore.GetById(contentId.Value) : 
                    UmbracoContext.Current.ContentCache.GetById(contentId.Value);
            }

            filterContext.HttpContext.Items[Constants.TemplateContentKey] = content;
        }
    }
}