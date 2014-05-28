using Umbraco.Web;
using Umbraco.Web.Routing;

namespace ReturnNull.UmbracoApi.Wrappers
{
    /// <summary>
    /// A concrete wrapper over the UmbracoContext providing access
    /// to simple UmbracoContext properties.
    /// </summary>
    public class PseudoUmbracoContext : IUmbracoContext
    {
        private readonly UmbracoContext _context;

        public PseudoUmbracoContext(UmbracoContext context)
        {
            _context = context;
        }

        public bool IsFrontEndUmbracoRequest
        {
            get { return _context.IsFrontEndUmbracoRequest; }
        }

        public PublishedContentRequest PublishedContentRequest
        {
            get { return _context.PublishedContentRequest; }
            set { _context.PublishedContentRequest = value; }
        }

        public bool IsDebug
        {
            get { return _context.IsDebug; }
        }

        public int? PageId
        {
            get { return _context.PageId; }
        }

        public bool InPreviewMode
        {
            get { return _context.InPreviewMode; }
            set { _context.InPreviewMode = value; }
        }
    }
}