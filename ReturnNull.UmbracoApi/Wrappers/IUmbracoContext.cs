using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using umbraco.BusinessLogic;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace ReturnNull.UmbracoApi.Wrappers
{

    /// <summary>
    /// Provides access to basic statistical data about the current
    /// state of the UmbracoContext
    /// </summary>
    internal interface IUmbracoContext
    {
        /// <summary>
        /// Boolean value indicating whether the current request is a front-end umbraco request
        /// 
        /// </summary>
        bool IsFrontEndUmbracoRequest { get; }

        /// <summary>
        /// Gets/sets the PublishedContentRequest object
        /// 
        /// </summary>
        PublishedContentRequest PublishedContentRequest { get; set; }

        /// <summary>
        /// Gets a value indicating whether the request has debugging enabled
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>true</c> if this instance is debug; otherwise, <c>false</c>.
        /// </value>
        bool IsDebug { get; }

        /// <summary>
        /// Gets the current page ID, or <c>null</c> if no page ID is available (e.g. a custom page).
        /// 
        /// </summary>
        int? PageId { get; }

        /// <summary>
        /// Determines whether the current user is in a preview mode and browsing the site (ie. not in the admin UI)
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Can be internally set by the RTE macro rendering to render macros in the appropriate mode.
        /// </remarks>
        bool InPreviewMode { get; set; }
    }
}
