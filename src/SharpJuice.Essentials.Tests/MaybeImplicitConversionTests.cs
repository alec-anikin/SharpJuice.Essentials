using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeImplicitConversionTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData("Test")]
        public void AssigningValueToMaybe_ValueImplicitlyConverted<T>(T value)
        {
            Maybe<T> maybe = value;

            maybe.Should().ContainSingle().Subject.Should().Be(value);
        }

        [Fact]
        public void AssigningNullableEmptyValueToMaybe_ValueImplicitlyConverted()
        {
            Maybe<int?> maybe = default(int?);

            maybe.Should().BeEmpty();
        }

        [Fact]
        public void AssigningNullValueToMaybeStruct_ValueImplicitlyConverted()
        {
            Maybe<int?> maybe = null;

            maybe.Should().BeEmpty();
        }

        [Fact]
        public void AssigningNullValueToMaybe_ValueImplicitlyConverted()
        {
            Maybe<string> maybe = null;

            maybe.Should().BeEmpty();
        }

        [Fact]
        public void AssigningNullableValueToMaybe_ValueImplicitlyConverted()
        {
            Maybe<int?> maybe = (int?)10;

            maybe.Should().ContainSingle(v => v == 10);
        }
        
        [Fact]
        public void InvokingGenericMethodWithMaybeParameter_ParameterImplicitlyConverted()
        {
            var maybe = F<int>(10);

            maybe.Should().ContainSingle(v => v == 10);
        }

        [Fact]
        public void InvokingMethodWithMaybeParameter_ParameterImplicitlyConverted()
        {
            var maybe = F("Test");

            maybe.Should().ContainSingle(v => v == "Test");
        }

        private Maybe<T> F<T>(Maybe<T> maybe) => maybe;

        private Maybe<string> F(Maybe<string> maybe) => maybe;
    }
}