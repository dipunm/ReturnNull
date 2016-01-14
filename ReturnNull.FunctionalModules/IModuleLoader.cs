using System.Collections.Generic;

namespace ReturnNull.FunctionalModules
{
    /// <summary>
    /// When implemented, will execute or use the provided modules
    /// to create a composed container for use in your application.
    /// </summary>
    /// <typeparam name="TModule">
    ///     The type of object that this class can execute or compose 
    ///     in order to create the desired container.
    /// </typeparam>
    /// <typeparam name="TContainer">
    ///     The container that this module loader will create. The 
    ///     container will provide your modular dependencies during 
    ///     runtime.
    /// </typeparam>
    public interface IModuleLoader<in TModule, out TContainer>
    {
        TContainer LoadModules(IEnumerable<TModule> modules);
    }
}