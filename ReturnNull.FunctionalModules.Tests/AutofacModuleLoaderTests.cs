using Autofac;
using NUnit.Framework;
using ReturnNull.FunctionModules.AutofacLoader;

namespace ReturnNull.FunctionalModules.Tests
{
    [TestFixture]
    class AutofacModuleLoaderTests
    {
        private interface IDependency
        {
        }

        private class Dependency : IDependency
        {
            
        }

        public class TestModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<Dependency>().As<IDependency>();
                base.Load(builder);
            }
        }

        private enum Functions { Function }

        [Test]
        public void LoadModules_GivenAnAutofacModule_ShouldProvideAContainerWhichCanResolveDependencies()
        {
            var loader = new AutofacModuleLoader();
            var container = loader.LoadModules(new[] {new TestModule()});
            var dependency = container.Resolve<IDependency>();
            Assert.IsInstanceOf<Dependency>(dependency);
        }

        [Test]
        public void ModuleComposer_GivenAutofacModuleLoader_ShouldProvideAContainerWhichCanResolveDependencies()
        {
            var container = ModuleComposer< Functions, Module, IContainer>.Compose(new AutofacModuleLoader(), designator =>
            {
                designator.Use<TestModule>(Functions.Function);
            });

            var dependency = container.Resolve<IDependency>();
            Assert.IsInstanceOf<Dependency>(dependency);
        }

    }


}
