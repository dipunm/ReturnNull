using System.Collections.Generic;
using Autofac;
using ReturnNull.FunctionalModules;

namespace ReturnNull.FunctionModules.AutofacLoader
{
    public class AutofacModuleLoader : IModuleLoader<Module, IContainer>
    {
        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public IContainer LoadModules(IEnumerable<Module> modules)
        {
            foreach (var module in modules)
            {
                _builder.RegisterModule(module);
            }

            return _builder.Build();
        }
    }
}
