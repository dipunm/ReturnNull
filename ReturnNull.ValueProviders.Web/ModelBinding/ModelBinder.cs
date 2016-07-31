using System.Web.Mvc;

namespace ReturnNull.ValueProviders.Web.ModelBinding
{
    public class ModelBinder<TBuilder, TModel> : DefaultModelBinder where TBuilder : IModelBuilder<TModel>
    {
        private readonly TBuilder _builder;

        public ModelBinder(TBuilder builder)
        {
            _builder = builder;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if(bindingContext.ModelType == typeof(TModel))
                return _builder.BuildModel(DataSourceConfig.BuildValueProvider(controllerContext, bindingContext));

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}