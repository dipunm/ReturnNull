using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;

namespace ReturnNull.FunctionalModules.Tests
{
    [TestFixture]
    public class ModuleComposerTests
    {
        enum NoFunctions
        {
        }

        enum SomeFunctions
        {
            Function1, Function2
        }

        [Test]
        public void Composer_WhenProvidedNullModuleLoader_ThrowsArgumentException()
        {
            var mockDesignation = new Action<IModuleDesignator<NoFunctions, object>>(designator => { });
            Assert.Throws<ArgumentNullException>(
                () => ModuleComposer<NoFunctions, object, object>.Compose(null, mockDesignation));
        }

        [Test]
        public void Composer_WhenProvidedNullDesignationDelegate_ThrowsArgumentException()
        {
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();
            Assert.Throws<ArgumentNullException>(
                () => ModuleComposer<NoFunctions, object, object>.Compose(mockModuleLoader.Object, null));
        }

        [Test]
        public void Composer_WhenProvidedDesignationDelegate_ExecutesDelegate()
        {
            bool wasCalled = false;
            var mockDesignation = new Action<IModuleDesignator<NoFunctions, object>>(designator =>
            {
                wasCalled = true;
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();

            ModuleComposer<NoFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void Composer_WhenTFunctionIsNotEnum_ThrowsInvalidCastException()
        {
            var mockDesignation = new Action<IModuleDesignator<object, object>>(designator => { });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();

            Assert.Throws<InvalidCastException>(
                () => ModuleComposer<object, object, object>.Compose(mockModuleLoader.Object, mockDesignation));
        }

        [Test]
        public void Composer_WhenDesignationDelegateDoesNotDefineAModuleForAllFunctions_ThrowsInvalidOperationException()
        {
            var mockDesignation = new Action<IModuleDesignator<SomeFunctions, object>>(designator =>
            {
                designator.Use<object>(SomeFunctions.Function1);
                //designator.Use<object>(SomeFunctions.Function2); //COMMENTED ON PURPOSE
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();

            Assert.Throws<InvalidOperationException>(
                () => ModuleComposer<SomeFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation));
        }

        [Test]
        public void Composer_WhenAllFunctionalModulesProvided_ProvidesAllModulesToModuleLoader()
        {
            var module1 = new object();
            var module2 = new object();
            var mockDesignation = new Action<IModuleDesignator<SomeFunctions, object>>(designator =>
            {
                designator.Use(module1, SomeFunctions.Function1);
                designator.Use(module2, SomeFunctions.Function2);
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();

            ModuleComposer<SomeFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation);

            mockModuleLoader.Verify(
                LoadModulesRecievedEnumerableWith(module1, module2));
        }

        [Test]
        public void Composer_WhenFunctionDefinedByMultipleModules_ProvidesLastDefinedModulesForEachFunctionToModuleLoader()
        {
            var module1 = new object();
            var module2 = new object();
            var module3 = new object();
            var module4 = new object();
            var mockDesignation = new Action<IModuleDesignator<SomeFunctions, object>>(designator =>
            {
                designator.Use(module1, SomeFunctions.Function1);
                designator.Use(module2, SomeFunctions.Function2);
                designator.Use(module3, SomeFunctions.Function1);
                designator.Use(module4, SomeFunctions.Function2);
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();

            ModuleComposer<SomeFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation);

            mockModuleLoader.Verify(
                LoadModulesRecievedEnumerableWith(module3, module4));
        }

        [Test]
        public void Composer_WhenAllFunctionsDefined_ReturnsContainerCreatedByModuleLoader()
        {
            var module1 = new object();
            var module2 = new object();
            var mockContainer = new object();
            var mockDesignation = new Action<IModuleDesignator<SomeFunctions, object>>(designator =>
            {
                designator.Use(module1, SomeFunctions.Function1);
                designator.Use(module2, SomeFunctions.Function2);
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();
            mockModuleLoader.Setup(loader => loader.LoadModules(It.IsAny<IEnumerable<object>>()))
                .Returns(mockContainer);

            var container = ModuleComposer<SomeFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation);
            
            Assert.AreSame(mockContainer, container);
        }

        [Test]
        public void Composer_WhenAllFunctionsDefinedUsingGenericMethod_EachModuleIsUnique()
        {
            var mockDesignation = new Action<IModuleDesignator<SomeFunctions, object>>(designator =>
            {
                designator.Use<object>(SomeFunctions.Function1);
                designator.Use<object>(SomeFunctions.Function2);
            });
            var mockModuleLoader = new Mock<IModuleLoader<object, object>>();
            
            ModuleComposer<SomeFunctions, object, object>.Compose(mockModuleLoader.Object, mockDesignation);

            mockModuleLoader.Verify(loader => loader.LoadModules(It.Is(Distinct())));
        }

        private Expression<Func<IEnumerable<object>,bool>> Distinct()
        {
            return objects => objects.Distinct().SequenceEqual(objects);
        }

        private static System.Linq.Expressions.Expression<Func<IModuleLoader<object, object>, object>> LoadModulesRecievedEnumerableWith(params object[] expectedModules)
        {
            return l => l.LoadModules(It.Is<IEnumerable<object>>(modules => expectedModules.SequenceEqual(modules)));
        }
    }
}