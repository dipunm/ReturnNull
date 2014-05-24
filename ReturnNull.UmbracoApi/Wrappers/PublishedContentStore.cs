using System.Collections.Generic;
using System.Xml.XPath;
using Umbraco.Core.Models;
using Umbraco.Core.Xml;
using Umbraco.Web.PublishedCache;

namespace ReturnNull.UmbracoApi.Wrappers
{
    /// <summary>
    /// A wrapper around the ContextualPublishedContentCache. To add functionality,
    /// you may simply extend this class and override any method.
    /// </summary>
    public class PublishedContentStore : IPublishedContentStore
    {
        private readonly ContextualPublishedContentCache _contentCache;

        public PublishedContentStore(ContextualPublishedContentCache contentCache)
        {
            _contentCache = contentCache;
        }

        /// <summary>
        /// Gets content identified by a route.
        /// 
        /// </summary>
        /// <param name="route">The route</param><param name="hideTopLevelNode">A value forcing the HideTopLevelNode setting.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// A valid route is either a simple path eg <c>/foo/bar/nil</c> or a root node id and a path, eg <c>123/foo/bar/nil</c>.
        /// </para>
        /// 
        /// <para>
        /// Considers published or unpublished content depending on context.
        /// </para>
        /// 
        /// </remarks>
        public virtual IPublishedContent GetByRoute(string route, bool? hideTopLevelNode)
        {
            return _contentCache.GetByRoute(route, hideTopLevelNode);
        }

        /// <summary>
        /// Gets content identified by a route.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="route">The route</param><param name="hideTopLevelNode">A value forcing the HideTopLevelNode setting.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// A valid route is either a simple path eg <c>/foo/bar/nil</c> or a root node id and a path, eg <c>123/foo/bar/nil</c>.
        /// </remarks>
        public virtual IPublishedContent GetByRoute(bool preview, string route, bool? hideTopLevelNode)
        {
            return _contentCache.GetByRoute(preview, route, hideTopLevelNode);
        }

        /// <summary>
        /// Gets the route for a content identified by its unique identifier.
        /// 
        /// </summary>
        /// <param name="contentId">The content unique identifier.</param>
        /// <returns>
        /// The route.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual string GetRouteById(int contentId)
        {
            return _contentCache.GetRouteById(contentId);
        }

        /// <summary>
        /// Gets the route for a content identified by its unique identifier.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="contentId">The content unique identifier.</param>
        /// <returns>
        /// The route.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual string GetRouteById(bool preview, int contentId)
        {
            return _contentCache.GetRouteById(preview, contentId);
        }

        /// <summary>
        /// Gets a content identified by its unique identifier.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="contentId">The content unique identifier.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        public virtual IPublishedContent GetById(bool preview, int contentId)
        {
            return _contentCache.GetById(preview, contentId);
        }

        /// <summary>
        /// Gets content at root.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        public virtual IEnumerable<IPublishedContent> GetAtRoot(bool preview)
        {
            return _contentCache.GetAtRoot(preview);
        }

        /// <summary>
        /// Gets a content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// </remarks>
        public virtual IPublishedContent GetSingleByXPath(bool preview, string xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetSingleByXPath(preview, xpath, vars);
        }

        /// <summary>
        /// Gets a content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// </remarks>
        public virtual IPublishedContent GetSingleByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetSingleByXPath(preview, xpath, vars);
        }

        /// <summary>
        /// Gets content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// </remarks>
        public virtual IEnumerable<IPublishedContent> GetByXPath(bool preview, string xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetByXPath(preview, xpath, vars);
        }

        /// <summary>
        /// Gets content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// </remarks>
        public virtual IEnumerable<IPublishedContent> GetByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetByXPath(preview, xpath, vars);
        }

        /// <summary>
        /// Gets an XPath navigator that can be used to navigate content.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// The XPath navigator.
        /// </returns>
        public virtual XPathNavigator GetXPathNavigator(bool preview)
        {
            return _contentCache.GetXPathNavigator(preview);
        }

        /// <summary>
        /// Gets a value indicating whether the underlying non-contextual cache contains content.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// A value indicating whether the underlying non-contextual cache contains content.
        /// </returns>
        public virtual bool HasContent(bool preview)
        {
            return _contentCache.HasContent(preview);
        }

        /// <summary>
        /// Gets the underlying published cache.
        /// 
        /// </summary>
        public virtual IPublishedContentCache InnerCache
        {
            get { return _contentCache.InnerCache; }
        }

        /// <summary>
        /// Gets a value indicating whether <c>GetXPathNavigator</c> returns an <c>XPathNavigator</c>
        ///             and that navigator is a <c>NavigableNavigator</c>.
        /// 
        /// </summary>
        public virtual bool XPathNavigatorIsNavigable
        {
            get { return _contentCache.XPathNavigatorIsNavigable; }
        }

        /// <summary>
        /// Informs the contextual cache that content has changed.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// The contextual cache may, although that is not mandatory, provide an immutable snapshot of
        ///             the content over the duration of the context. If you make changes to the content and do want to have
        ///             the cache update its snapshot, you have to explicitely ask it to do so by calling ContentHasChanged.
        /// </remarks>
        public virtual void ContentHasChanged()
        {
            _contentCache.ContentHasChanged();
        }

        /// <summary>
        /// Gets a content identified by its unique identifier.
        /// 
        /// </summary>
        /// <param name="contentId">The content unique identifier.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual IPublishedContent GetById(int contentId)
        {
            return _contentCache.GetById(contentId);
        }

        /// <summary>
        /// Gets content at root.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The contents.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual IEnumerable<IPublishedContent> GetAtRoot()
        {
            return _contentCache.GetAtRoot();
        }

        /// <summary>
        /// Gets a content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// <para>
        /// Considers published or unpublished content depending on context.
        /// </para>
        /// 
        /// </remarks>
        public virtual IPublishedContent GetSingleByXPath(string xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetSingleByXPath(xpath, vars);
        }

        /// <summary>
        /// Gets a content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// <para>
        /// Considers published or unpublished content depending on context.
        /// </para>
        /// 
        /// </remarks>
        public virtual IPublishedContent GetSingleByXPath(XPathExpression xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetSingleByXPath(xpath, vars);
        }

        /// <summary>
        /// Gets content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// <para>
        /// Considers published or unpublished content depending on context.
        /// </para>
        /// 
        /// </remarks>
        public virtual IEnumerable<IPublishedContent> GetByXPath(string xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetByXPath(xpath, vars);
        }

        /// <summary>
        /// Gets content resulting from an XPath query.
        /// 
        /// </summary>
        /// <param name="xpath">The XPath query.</param><param name="vars">Optional XPath variables.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        /// 
        /// <remarks>
        /// 
        /// <para>
        /// If <param name="vars"/> is <c>null</c>, or is empty, or contains only one single
        ///             value which itself is <c>null</c>, then variables are ignored.
        /// </para>
        /// 
        /// <para>
        /// The XPath expression should reference variables as <c>$var</c>.
        /// </para>
        /// 
        /// <para>
        /// Considers published or unpublished content depending on context.
        /// </para>
        /// 
        /// </remarks>
        public virtual IEnumerable<IPublishedContent> GetByXPath(XPathExpression xpath, params XPathVariable[] vars)
        {
            return _contentCache.GetByXPath(xpath, vars);
        }

        /// <summary>
        /// Gets an XPath navigator that can be used to navigate content.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// The XPath navigator.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual XPathNavigator GetXPathNavigator()
        {
            return _contentCache.GetXPathNavigator();
        }

        /// <summary>
        /// Gets a value indicating whether the underlying non-contextual cache contains content.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A value indicating whether the underlying non-contextual cache contains content.
        /// </returns>
        /// 
        /// <remarks>
        /// Considers published or unpublished content depending on context.
        /// </remarks>
        public virtual bool HasContent()
        {
            return _contentCache.HasContent();
        }
    }
}
