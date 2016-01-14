using System;
using System.Collections.Generic;
using System.Linq;

namespace ReturnNull.FunctionalModules
{
    /// <summary>
    /// Module composer will compose your application enforcing that all functions are defined by modules. 
    /// A function will be composed using only the last module defined for that function.
    /// </summary>
    /// <typeparam name="TFunction">An enum representing the functions that should be defined using modules.</typeparam>
    /// <typeparam name="TModule">An interface or base class representing the type that a module must implement in order to define a function.</typeparam>
    /// <typeparam name="TContainer">The container type that will be created when the modules have been composed.</typeparam>
    public sealed class ModuleComposer<TFunction, TModule, TContainer> : IModuleDesignator<TFunction, TModule> where TModule : class
    {
        private readonly IModuleLoader<TModule, TContainer> _moduleLoader;

        private readonly Dictionary<TFunction, TModule> _modules;

        private ModuleComposer(IModuleLoader<TModule, TContainer> moduleLoader)
        {
            if (!typeof(TFunction).IsEnum)
            {
                throw new InvalidCastException("Provided TEnum must be an Enum.");
            }

            _modules = new Dictionary<TFunction, TModule>();
            _moduleLoader = moduleLoader;

            //initialise _modules as a dictionary with all enum values as a key and null as the value for all.
            foreach (var moduleType in Enum.GetValues(typeof(TFunction)).OfType<TFunction>())
            {
                Use(null, moduleType);
            }

        }

        public void Use<TNewableModule>(TFunction moduleType) where TNewableModule : TModule, new()
        {
            Use(new TNewableModule(), moduleType);
        }

        public void Use(TModule module, TFunction moduleType)
        {
            _modules[moduleType] = module;
        }

        public TContainer CreateContainer()
        {
            if (_modules.ContainsValue(null))
            {
                throw new InvalidOperationException(
                    "Not all modules have been defined. Please ensure all modules have been defined " +
                    "before creating the container.");
            }

            return _moduleLoader.LoadModules(_modules.Values);
        }

        public static TContainer Compose(IModuleLoader<TModule, TContainer> moduleLoader, Action<IModuleDesignator<TFunction, TModule>> designation)
        {
            if (moduleLoader == null) throw new ArgumentNullException(nameof(moduleLoader));
            if (designation == null) throw new ArgumentNullException(nameof(designation));

            var composer = new ModuleComposer<TFunction, TModule, TContainer>(moduleLoader);
            designation.Invoke(composer);
            return composer.CreateContainer();
        }
    }
}