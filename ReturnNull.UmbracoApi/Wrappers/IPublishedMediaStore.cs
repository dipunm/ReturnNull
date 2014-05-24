using System.Collections.Generic;
using System.Xml.XPath;
using Umbraco.Core.Models;
using Umbraco.Core.Xml;
using Umbraco.Web.PublishedCache;

namespace ReturnNull.UmbracoApi.Wrappers
{
    /// <summary>
    /// This interface has been generated based on the Umbraco ContextualPublishedMediaCache.
    /// The implementation of this interface will wrap Umbraco's ContextualPublishedMediaCache allowing
    /// use of the cache which depends on the UmbracoContext, whilst using the interface will 
    /// allow easy mocking.
    /// </summary>
    internal interface IPublishedMediaStore
    {
        IPublishedContent GetById(bool preview, int contentId);
        IEnumerable<IPublishedContent> GetAtRoot(bool preview);
        IPublishedContent GetSingleByXPath(bool preview, string xpath, params XPathVariable[] vars);
        IPublishedContent GetSingleByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars);
        IEnumerable<IPublishedContent> GetByXPath(bool preview, string xpath, params XPathVariable[] vars);
        IEnumerable<IPublishedContent> GetByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars);
        XPathNavigator GetXPathNavigator(bool preview);
        bool HasContent(bool preview);
        IPublishedMediaCache InnerCache { get; }
        bool XPathNavigatorIsNavigable { get; }
        void ContentHasChanged();
        IPublishedContent GetById(int contentId);
        IEnumerable<IPublishedContent> GetAtRoot();
        IPublishedContent GetSingleByXPath(string xpath, params XPathVariable[] vars);
        IPublishedContent GetSingleByXPath(XPathExpression xpath, params XPathVariable[] vars);
        IEnumerable<IPublishedContent> GetByXPath(string xpath, params XPathVariable[] vars);
        IEnumerable<IPublishedContent> GetByXPath(XPathExpression xpath, params XPathVariable[] vars);
        XPathNavigator GetXPathNavigator();
        bool HasContent();
    }
}
