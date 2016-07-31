using System.Web.Mvc;

namespace ReturnNull.ValueProviders.Web
{
    public interface IValueSourceProvider
    {
        IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext);
    }
}