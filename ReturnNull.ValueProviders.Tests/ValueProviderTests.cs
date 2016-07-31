using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ReturnNull.ValueProviders.Tests
{
    [TestFixture]
    public class ValueProviderTests
    {
        [Test]
        public void ValueProvider_WhenNotGivenValueSources_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new ValueProvider(null));
        }

        [Test]
        public void ValueProvider_WhenGivenEmptySetOfValueSources_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ValueProvider(new Dictionary<string, IValueSource>()));
        }

        [Test]
        public void GetValues_GivenMultipleValueSources_ShouldGetValuesFromAllSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks);
            var provider = new ValueProvider(sources);

            provider.GetValues<int>("key").ToList();

            mocks.ForEach(source => source.Verify(s => s.GetValues<int>("key"), Times.Once));
        }

        [Test]
        public void GetValues_GivenMultipleValueSources_ShouldReturnAllValuesFromAllSources()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 2,4,0) },
                { "source2", MockValueSource.With("key", 3) },
                { "source3", MockValueSource.With("key", 9,20,3) }
            };
            var provider = new ValueProvider(sources);

            var values = provider.GetValues<int>("key");

            values.ShouldBe(new[] { 2, 4, 0, 3, 9, 20, 3 });
        }

        [Test]
        public void GetValue_GivenMultipleValueSources_ShouldReturnFirstValueFromFirstValueSource()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 2,4,0) },
                { "source2", MockValueSource.With("key", 3) },
                { "source3", MockValueSource.With("key", 9,20,3) }
            };
            var provider = new ValueProvider(sources);

            var value = provider.GetValue<int>("key");

            value.ShouldBe(2);
        }

        [Test]
        public void GetValue_GivenMultipleValueSources_IncludingSourcesWithNoRelevantData_ShouldReturnFirstValueFromFirstValueSource()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key") }, //no data
                { "source2", MockValueSource.With("key", 3) },
                { "source3", MockValueSource.With("key", 9,20,3) }
            };
            var provider = new ValueProvider(sources);

            var value = provider.GetValue<int>("key");

            value.ShouldBe(3);
        }

        [Test]
        public void GetValue_WhenNoValuesFound_ShouldReturnDefaultValue()
        {
            const int defaultValue = 1;
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key") } //no data
            };
            var provider = new ValueProvider(sources);

            var value = provider.GetValue("key", defaultValue);

            value.ShouldBe(defaultValue);
        }

        [Test]
        public void GetValue_GivenMultipleSources_WhenFirstSourceHasData_ShouldNotAskOtherValueSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            mocks.First().Setup(m => m.GetValues<int>("key"))
                .Returns(new[] { 1 });
            var sources = ToDictionary(mocks);
            var provider = new ValueProvider(sources);

            provider.GetValue<int>("key");

            mocks.Skip(1).ToList()
                .ForEach(m => m.Verify(s => s.GetValues<int>(It.IsAny<string>()), Times.Never));
        }

        [Test]
        public void TryGetValue_WhenNoDataFound_ShouldReturnFalse()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key") } //no data
            };
            var provider = new ValueProvider(sources);

            int outVal;
            var result = provider.TryGetValue("key", out outVal);

            result.ShouldBe(false);
        }

        [Test]
        public void TryGetValue_WhenValueFound_ShouldReturnTrue()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 1) }
            };
            var provider = new ValueProvider(sources);

            int outVal;
            var result = provider.TryGetValue("key", out outVal);

            result.ShouldBe(true);
        }

        [Test]
        public void TryGetValue_WhenValueFound_AndValueIsDefaultT_ShouldReturnTrue()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", default(int)) }
            };
            var provider = new ValueProvider(sources);

            int outVal;
            var result = provider.TryGetValue("key", out outVal);

            result.ShouldBe(true);
        }

        [Test]
        public void TryGetValue_WhenValueFound_ShouldOutputValue()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 1) }
            };
            var provider = new ValueProvider(sources);

            int outVal;
            provider.TryGetValue("key", out outVal);

            outVal.ShouldBe(1);
        }

        [Test]
        public void Including_ShouldReturnNewObject()
        {
            var sources = new Dictionary<string, IValueSource>()
            {
                { "source1", MockValueSource.With("key") },
                { "source2", MockValueSource.With("key") },
                { "source3", MockValueSource.With("key") }
            };
            var provider = new ValueProvider(sources);

            var newProvider = provider.Excluding("source1");

            newProvider.ShouldNotBe(provider);
        }

        [Test]
        public void Excluding_ShouldCreateProviderThatWillNotGetValuesFromSpecifiedSource()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            provider.Excluding("source1").GetValues<int>("key").ToList();

            mocks.ElementAt(0).Verify(s => s.GetValues<int>("key"), Times.Never);
            mocks.ElementAt(1).Verify(s => s.GetValues<int>("key"), Times.Once);
            mocks.ElementAt(2).Verify(s => s.GetValues<int>("key"), Times.Once);
        }

        [Test]
        public void Excluding_GivenMultipleSourcesToExclude_ShouldCreateProviderThatWillNotGetValuesFromSpecifiedSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            provider.Excluding("source1", "source3").GetValues<int>("key").ToList();

            mocks.ElementAt(0).Verify(s => s.GetValues<int>("key"), Times.Never);
            mocks.ElementAt(1).Verify(s => s.GetValues<int>("key"), Times.Once);
            mocks.ElementAt(2).Verify(s => s.GetValues<int>("key"), Times.Never);
        }

        [Test]
        public void LimitedTo_ShouldCreateProviderThatWillOnlyGetValuesFromSpecifiedSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            provider.LimitedTo("source1", "source3").GetValues<int>("key").ToList();

            mocks.ElementAt(0).Verify(s => s.GetValues<int>("key"), Times.Once);
            mocks.ElementAt(1).Verify(s => s.GetValues<int>("key"), Times.Never);
            mocks.ElementAt(2).Verify(s => s.GetValues<int>("key"), Times.Once);
        }

        [Test]
        public void LimitedTo_GivenANonExistingSource_ShouldThrowInvalidOperationExceptionSpecifyingNameOfBadSourceInErrorMessage()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            var ex = Assert.Throws<ArgumentException>(() => provider.LimitedTo("source100"));
            ex.Message.ShouldContain("source100");
        }

        [Test]
        public void LimitedTo_GivenManyNonExistingSources_ShouldThrowInvalidOperationExceptionSpecifyingAllNamesOfBadSourcesInErrorMessage()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            var ex = Assert.Throws<ArgumentException>(() => provider.LimitedTo("source100", "source1", "source4"));
            ex.Message.ShouldContain("'source100'");
            ex.Message.ShouldContain("'source4'");
            ex.Message.ShouldNotContain("'source1'");
        }

        [Test]
        public void Preferring_GivenPreferredSources_ShouldCreateProviderThatChecksPreferredSourcesForValuesBeforeOthers()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 1) },
                { "source2", MockValueSource.With("key", 2) },
                { "source3", MockValueSource.With("key", 3) }
            };
            var provider = new ValueProvider(sources);

            var value = provider.Preferring("source3").GetValue<int>("key");

            value.ShouldBe(3);
        }

        [Test]
        public void Preferring_GivenPreferredSources_ShouldCreateProviderThatContainsAllValueSources()
        {
            var sources = new Dictionary<string, IValueSource>
            {
                { "source1", MockValueSource.With("key", 1) },
                { "source2", MockValueSource.With("key", 2) },
                { "source3", MockValueSource.With("key", 3) }
            };
            var provider = new ValueProvider(sources);

            var value = provider.Preferring("source3").GetValues<int>("key");

            value.ShouldBe(new[] { 1,2,3 }, ignoreOrder: true);
        }

        [Test]
        public void Preferring_GivenManyNonExistingSources_ShouldThrowInvalidOperationExceptionSpecifyingAllNamesOfBadSourcesInErrorMessage()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks, new[] { "source1", "source2", "source3" });
            var provider = new ValueProvider(sources);

            var ex = Assert.Throws<ArgumentException>(() => provider.Preferring("source100", "source1", "source4"));
            ex.Message.ShouldContain("'source100'");
            ex.Message.ShouldContain("'source4'");
            ex.Message.ShouldNotContain("'source1'");
        }

        [Test]
        public void GetValue_GivenAKey_ShouldSendKeyToValueSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks);
            var provider = new ValueProvider(sources);

            provider.GetValue<int>("uniqueKey");

            mocks.ToList().ForEach(m => m.Verify(s => s.GetValues<int>("uniqueKey")));
        }

        [Test]
        public void TryGetValue_GivenAKey_ShouldSendKeyToValueSources()
        {
            var mocks = new List<Mock<IValueSource>>()
            {
                new Mock<IValueSource>(),
                new Mock<IValueSource>(),
                new Mock<IValueSource>()
            };
            var sources = ToDictionary(mocks);
            var provider = new ValueProvider(sources);

            int value;
            provider.TryGetValue("uniqueKey", out value);

            mocks.ToList().ForEach(m => m.Verify(s => s.GetValues<int>("uniqueKey")));
        }

        public class MockValueSource : IValueSource
        {
            public static MockValueSource With(string key, params object[] values)
            {
                return new MockValueSource(new Dictionary<string, IEnumerable<object>>()
                { {"key",values} });
            }

            private readonly Dictionary<string, IEnumerable<object>> _data;

            public MockValueSource(Dictionary<string, IEnumerable<object>> data)
            {
                _data = data;
            }

            public IEnumerable<T> GetValues<T>(string key)
            {
                return _data["key"].OfType<T>();
            }
        }

        private static IDictionary<string, IValueSource> ToDictionary(List<Mock<IValueSource>> mocks, string[] keys = null)
        {
            return mocks.Aggregate(new Dictionary<string, IValueSource>(),
                (dictionary, mock) =>
                {
                    var key = keys?.ElementAtOrDefault(dictionary.Count) ?? "source" + (dictionary.Count + 1);
                    dictionary.Add(key, mock.Object);
                    return dictionary;
                });
        }


    }
}
