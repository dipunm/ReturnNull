using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Umbraco.Core.Configuration;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace ReturnNull.UmbracoMvcCatalyst
{
    /// <summary>
    /// Provides a Model agnostic base page for umbraco templates.
    /// This base page does not provide easy access to Umbraco helpers.
    /// This base page will allow rendering of the Umbraco preview badge or profiler.
    /// </summary>
    /// <remarks>
    /// Umbraco helpers cause problems when being used by standard MVC pages.
    /// Since this page will be used for layouts, we do not want it to crash when an
    /// MVC page tries to re-use this file as its layout file.
    /// </remarks>
    public abstract class UmbracoMvcTemplatePage : WebViewPage
    {
        /// <summary>
        /// Provides an instance of the current page.
        /// If not on a valid page (eg. using MVC), it will provide
        /// a wrapper based around ViewData.
        /// </summary>
        public IPublishedContent Content
        {
            get { return Context.Items[Constants.TemplateContentKey] as IPublishedContent; }
        }

        /// <summary>
        /// This will detect the end /body tag and insert the preview badge if in preview mode
        /// 
        /// </summary>
        /// <param name="value"/>
        public override void WriteLiteral(object value)
        {
            if (Umbraco.Core.StringExtensions.InvariantEquals(this.Response.ContentType, "text/html") && (UmbracoContext.Current.IsDebug || UmbracoContext.Current.InPreviewMode))
            {
                string str1 = value.ToString().ToLowerInvariant();
                int index = str1.IndexOf("</body>", StringComparison.InvariantCultureIgnoreCase);
                if (index > -1)
                {
                    string str2 = !UmbracoContext.Current.InPreviewMode ? 
                        Html.RenderProfiler().ToHtmlString() : 
                        String.Format(UmbracoConfig.For.UmbracoSettings().Content.PreviewBadge, 
                            IOHelper.ResolveUrl(SystemDirectories.Umbraco),
                            IOHelper.ResolveUrl(SystemDirectories.UmbracoClient), 
                            Server.UrlEncode(UmbracoContext.Current.HttpContext.Request.Path)
                        );

                    var stringBuilder = new StringBuilder(str1);
                    stringBuilder.Insert(index, str2);
                    base.WriteLiteral(stringBuilder.ToString());
                    return;
                }
            }
            base.WriteLiteral(value);
        }
    }
}
