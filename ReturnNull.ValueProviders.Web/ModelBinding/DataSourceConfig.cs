using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReturnNull.ValueProviders.Web.ModelBinding
{
    public static class DataSourceConfig
    {
        public static IDictionary<string, IValueSourceProvider> DataSources { get; } 
            = new Dictionary<string, IValueSourceProvider>();


        public static ValueProvider BuildValueProvider(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            var sources = DataSources.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Build(controllerContext, modelBindingContext));

            return new ValueProvider(sources);
        }
    }
}
