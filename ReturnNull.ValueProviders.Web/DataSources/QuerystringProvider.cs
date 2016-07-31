using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace ReturnNull.ValueProviders.Web.DataSources
{
    public class FormDataProvider : IValueSourceProvider
    {
        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return new NameValuePairSource(controllerContext.HttpContext.Request.Form);
        }
    }
    public class QuerystringProvider : IValueSourceProvider
    {
        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return new NameValuePairSource(controllerContext.HttpContext.Request.QueryString);
        }

    }

    public class NameValuePairSource : IValueSource
    {
        private readonly NameValueCollection _queryString;

        public NameValuePairSource(NameValueCollection queryString)
        {
            _queryString = queryString;
        }

        public IEnumerable<T> GetValues<T>(string key)
        {
            return _queryString.GetValues(key)?
                .Where(val => TypeDescriptor.GetConverter(typeof(T)).IsValid(val))
                .Select(val => (T)TypeDescriptor.GetConverter(typeof(T))
                    .ConvertFromInvariantString(val)) ?? Enumerable.Empty<T>();
        }
    }

    public class ValueProvider : IValueSourceProvider
    {
        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return new ValueSource(modelBindingContext.ValueProvider);
        }

        public class ValueSource : IValueSource
        {
            private readonly System.Web.Mvc.IValueProvider _valueProvider;

            public ValueSource(System.Web.Mvc.IValueProvider valueProvider)
            {
                _valueProvider = valueProvider;
            }

            public IEnumerable<T> GetValues<T>(string key)
            {
                var value = _valueProvider.GetValue(key);
                if (value != null &&
                    value.RawValue.GetType() == typeof (T))
                    return new[] {(T) value.RawValue};

                return Enumerable.Empty<T>();
            }
        }
    }
}
