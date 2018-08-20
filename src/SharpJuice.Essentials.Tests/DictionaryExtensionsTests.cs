using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class DictionaryExtensionsTests
    {
        [Fact]
        public void GettingMissingValue_ReturnsEmpty()
        {
            var dict = new Dictionary<string, int>();

            var value = dict.GetValue("test");

            value.Should().BeEmpty();
        }

        [Fact]
        public void GettingValue_ReturnsWrapped()
        {
            const string key = "test";

            var dict = new Dictionary<string, int>
            {
                {key, int.MaxValue}
            };

            var value = dict.GetValue(key);

            value.Single().Should().Be(int.MaxValue);
        }
    }
}