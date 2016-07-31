using System.Web.Mvc;
using NUnit.Framework;
using ReturnNull.ValueProviders.Web;
using ReturnNull.ValueProviders.Web.ModelBinding;
using Shouldly;

namespace ReturnNull.ValueProviders.Tests.Web
{
    [TestFixture]
    public class DataSourceConfigTests
    {
        [Test]
        public void BuildValueProvider_GivenMultipleSourceBuilders_ValueProviderShouldContainEveryDataSource()
        {
            DataSourceConfig.DataSources.Clear();
            DataSourceConfig.DataSources.Add("source1", new MockSourceProvider(1));
            DataSourceConfig.DataSources.Add("source2", new MockSourceProvider(2));
            DataSourceConfig.DataSources.Add("source3", new MockSourceProvider(3));
            DataSourceConfig.DataSources.Add("source4", new MockSourceProvider(4));

            var valueProvider = DataSourceConfig.BuildValueProvider(null, null);

            valueProvider.GetValues<int>("key").ShouldBe(new[] {1, 2, 3, 4});
        }
    }

    public class MockSourceProvider : IValueSourceProvider
    {
        private readonly object _dummyValue;

        public MockSourceProvider(object dummyValue)
        {
            _dummyValue = dummyValue;
        }

        public IValueSource Build(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            return ValueProviderTests.MockValueSource.With("key", _dummyValue);
        }
    }
}
