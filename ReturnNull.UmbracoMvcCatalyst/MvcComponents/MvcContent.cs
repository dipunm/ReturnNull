using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ReturnNull.UmbracoApi.Wrappers;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache;

namespace ReturnNull.UmbracoMvcCatalyst
{
    public sealed class MvcContent : IPublishedContent
    {
        private readonly ControllerContext _controllerContext;
        private readonly ViewDataDictionary _viewData;

        public MvcContent(ControllerContext controllerContext)
        {
            _controllerContext = controllerContext;
            _viewData = controllerContext.Controller.ViewData;
        }

        public string Name
        {
            get { return _controllerContext.RouteData.DataTokens["action"] as string; }
        }

        public string UrlName
        {
            get { return Name; }
        }

        public ICollection<IPublishedProperty> Properties
        {
            get { 
                return _viewData
                    .Select(pair => new MvcProperty(pair.Key, pair.Value))
                    .Cast<IPublishedProperty>()
                    .ToList();
            }
        }

        public string Url
        {
            get { return _controllerContext.HttpContext.Request.Url.PathAndQuery; }
        }
        
        public object this[string alias]
        {
            get
            {
                var prop = GetProperty(alias);
                return prop == null ? null : prop.Value;
            }
        }
        
        public IPublishedProperty GetProperty(string alias)
        {
            return Properties.FirstOrDefault(p => p.PropertyTypeAlias == alias);
        }

        public IPublishedProperty GetProperty(string alias, bool recurse)
        {
            return GetProperty(alias); //MvcContent does not have parents to recurse over.
        }
        public int GetIndex()
        {
            return 1;
        }

        public IEnumerable<IPublishedContent> ContentSet 
        {
            get { return new List<IPublishedContent>() {this}; }
        }

        public PublishedContentType ContentType
        {
            get { return null; }
        }

        public int Id
        {
            get { return 0; }
        }

        public int TemplateId
        {
            get { return 0; }
        }

        public int SortOrder
        {
            get { return 0; }
        }

        public string DocumentTypeAlias
        {
            get { return "MvcPage"; }
        }

        public int DocumentTypeId
        {
            get { return 0; }
        }

        public string WriterName
        {
            get { return null; }
        }

        public string CreatorName
        {
            get { return null; }
        }

        public int WriterId
        {
            get { return 0; }
        }

        public int CreatorId
        {
            get { return 0; }
        }

        public string Path
        {
            get { return null; }
        }

        public DateTime CreateDate
        {
            get { return DateTime.Now; }
        }

        public DateTime UpdateDate
        {
            get { return DateTime.Now; }
        }

        public Guid Version
        {
            get { return Guid.Empty; }
        }

        public int Level
        {
            get { return 0; }
        }

        public PublishedItemType ItemType
        {
            get { return PublishedItemType.Content; }
        }

        public bool IsDraft
        {
            get { return false; }
        }

        public IPublishedContent Parent
        {
            get { return null; }
        }

        public IEnumerable<IPublishedContent> Children
        {
            get { return new List<IPublishedContent>(); }
        }
    }
}
