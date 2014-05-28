using Umbraco.Core.Models;

namespace ReturnNull.UmbracoMvcCatalyst.MvcComponents
{
    public class MvcProperty : IPublishedProperty
    {
        public MvcProperty(string key, object value)
        {
            PropertyTypeAlias = key;
            HasValue = value != null;
            DataValue = value;
            Value = value;
            XPathValue = value;
        }

        public string PropertyTypeAlias { get; private set; }
        public bool HasValue { get; private set; }
        public object DataValue { get; private set; }
        public object Value { get; private set; }
        public object XPathValue { get; private set; }
    }
}