namespace ReturnNull.FunctionalModules
{
    /// <summary>
    /// Provides an interface by which to define modules for each function.
    /// If multiple modules are defined for one function, the last defined 
    /// module will be used.
    /// </summary>
    /// <typeparam name="TFunction">An enum representing the functions that should be defined using modules.</typeparam>
    /// <typeparam name="TModule">An interface or base class representing the type that a module must implement in order to define a function.</typeparam>
    public interface IModuleDesignator<in TFunction, in TModule> where TModule : class
    {
        void Use<TNewableModule>(TFunction moduleType) where TNewableModule : TModule, new();
        void Use(TModule module, TFunction moduleType);
    }
}