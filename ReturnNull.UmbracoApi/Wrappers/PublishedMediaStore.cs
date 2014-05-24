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
    public class PublishedMediaStore : IPublishedMediaStore
    {
        private readonly ContextualPublishedMediaCache _mediaCache;

        public PublishedMediaStore(ContextualPublishedMediaCache mediaCache)
        {
            _mediaCache = mediaCache;
        }

        /// <summary>
        /// Gets a content identified by its unique identifier.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param><param name="contentId">The content unique identifier.</param>
        /// <returns>
        /// The content, or null.
        /// </returns>
        public IPublishedContent GetById(bool preview, int contentId)
        {
            return _mediaCache.GetById(preview, contentId);
        }

        /// <summary>
        /// Gets content at root.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// The contents.
        /// </returns>
        public IEnumerable<IPublishedContent> GetAtRoot(bool preview)
        {
            return _mediaCache.GetAtRoot(preview);
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
        public IPublishedContent GetSingleByXPath(bool preview, string xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetSingleByXPath(preview, xpath, vars);
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
        public IPublishedContent GetSingleByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetSingleByXPath(preview, xpath, vars);
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
        public IEnumerable<IPublishedContent> GetByXPath(bool preview, string xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetByXPath(preview, xpath, vars);
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
        public IEnumerable<IPublishedContent> GetByXPath(bool preview, XPathExpression xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetByXPath(preview, xpath, vars);
        }

        /// <summary>
        /// Gets an XPath navigator that can be used to navigate content.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// The XPath navigator.
        /// </returns>
        public XPathNavigator GetXPathNavigator(bool preview)
        {
            return _mediaCache.GetXPathNavigator(preview);
        }

        /// <summary>
        /// Gets a value indicating whether the underlying non-contextual cache contains content.
        /// 
        /// </summary>
        /// <param name="preview">A value indicating whether to consider unpublished content.</param>
        /// <returns>
        /// A value indicating whether the underlying non-contextual cache contains content.
        /// </returns>
        public bool HasContent(bool preview)
        {
            return _mediaCache.HasContent(preview);
        }

        /// <summary>
        /// Gets the underlying published cache.
        /// 
        /// </summary>
        public IPublishedMediaCache InnerCache
        {
            get { return _mediaCache.InnerCache; }
        }

        /// <summary>
        /// Gets a value indicating whether <c>GetXPathNavigator</c> returns an <c>XPathNavigator</c>
        ///             and that navigator is a <c>NavigableNavigator</c>.
        /// 
        /// </summary>
        public bool XPathNavigatorIsNavigable
        {
            get { return _mediaCache.XPathNavigatorIsNavigable; }
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
        public void ContentHasChanged()
        {
             _mediaCache.ContentHasChanged();
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
        public IPublishedContent GetById(int contentId)
        {
            _mediaCache.GetById(contentId);
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
        public IEnumerable<IPublishedContent> GetAtRoot()
        {
            return _mediaCache.GetAtRoot();
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
        public IPublishedContent GetSingleByXPath(string xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetSingleByXPath(xpath, vars);
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
        public IPublishedContent GetSingleByXPath(XPathExpression xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetSingleByXPath(xpath, vars);
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
        public IEnumerable<IPublishedContent> GetByXPath(string xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetByXPath(xpath, vars);
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
        public IEnumerable<IPublishedContent> GetByXPath(XPathExpression xpath, params XPathVariable[] vars)
        {
            return _mediaCache.GetByXPath(xpath, vars);
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
        public XPathNavigator GetXPathNavigator()
        {
            return _mediaCache.GetXPathNavigator();
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
        public bool HasContent()
        {
            return _mediaCache.HasContent();
        }
    }
}