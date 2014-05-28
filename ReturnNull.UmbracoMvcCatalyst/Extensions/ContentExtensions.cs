using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReturnNull.UmbracoMvcCatalyst.MvcComponents;
using Umbraco.Core.Models;

namespace ReturnNull.UmbracoMvcCatalyst.Extensions
{
    public static class ContentExtensions
    {
        public static bool IsMvcContent(this IPublishedContent content)
        {
            return content is MvcContent;
        }
    }
}
