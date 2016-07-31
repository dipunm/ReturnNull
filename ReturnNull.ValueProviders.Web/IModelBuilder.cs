namespace ReturnNull.ValueProviders.Web.ModelBinding
{
    public interface IModelBuilder<out TModel>
    {
        TModel BuildModel(ValueProvider valueProvider);
    }
}