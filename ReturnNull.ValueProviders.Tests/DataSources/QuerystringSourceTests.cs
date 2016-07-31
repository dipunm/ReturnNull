using System.Collections.Specialized;
using NUnit.Framework;
using ReturnNull.ValueProviders.Web.DataSources;
using Shouldly;

namespace ReturnNull.ValueProviders.Tests.DataSources
{
    [TestFixture]
    public class QuerystringSourceTests
    {
        [Test]
        public void GetValues_WhenQuerystringHasValueForQueriedKey_ShouldReturnValue()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "value"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<string>("key");

            result.ShouldBe(new [] { "value" });
        }

        [Test]
        public void GetValues_WhenQuerystringHasMultipleValuesForQueriedKey_ShouldReturnAllValues()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "value"},
                {"key", "value2"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<string>("key");

            result.ShouldBe(new[] { "value", "value2" });
        }

        [Test]
        public void GetValues_WhenQuerystringDoesNotHaveValueForQueriedKey_ShouldReturnEmptyList()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "value"},
                {"key", "value2"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<string>("anotherkey");

            result.ShouldBe(new string[0]);
        }

        [Test]
        public void GetValues_WhenAskingForNonString_ShouldConvertValuesToType()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "true"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<bool>("key");

            result.ShouldBe(new [] { true });
        }

        [Test]
        public void GetValues_WhenQuerystringHasValueThatWontMatchRequestedType_ShouldExcludeInvalidValueFromResult()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "true"},
                {"key", "false"},
                {"key", "fal"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<bool>("key");

            result.ShouldBe(new[] { true, false });
        }

        [Test]
        public void GetValues_GivenMultipleValuesWithDifferentKeys_ShouldOnlyReturnTheResultsThatMatchRequestedKeyAndType()
        {
            var querystring = new NameValueCollection()
            {
                {"key", "true"},
                {"key", "false"},
                {"anotherKey", "value"},
                {"amount", "100.03"}
            };
            var source = new NameValuePairSource(querystring);

            var result = source.GetValues<decimal>("amount");

            result.ShouldBe(new[] { 100.03m });
        }
    }
}
