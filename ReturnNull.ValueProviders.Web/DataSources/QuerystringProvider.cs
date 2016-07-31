using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ReturnNull.ValueProviders.Web.ModelBinding;

namespace ReturnNull.ValueProviders.Web.DataSources
{
    public class QuerystringProvider : IValueSourceProvider
    {
        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return new QuerystringSource(controllerContext.HttpContext.Request.QueryString);
        }

        public class QuerystringSource : IValueSource
        {
            private readonly NameValueCollection _queryString;

            public QuerystringSource(NameValueCollection queryString)
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
    }

    public class ValueProvider : IValueSourceProvider
    {
        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return new ValueSource(modelBindingContext.ValueProvider);
        }

        public class ValueSource : IValueSource
        {
            private readonly IValueProvider _valueProvider;

            public ValueSource(IValueProvider valueProvider)
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
