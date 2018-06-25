using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeCreatingTests
    {
        [Fact]
        public void CreatingEmptyRef_EnumerableIsEmpty()
        {
            var mb = new Maybe<object>();

            mb.Should().BeEmpty();
        }

        [Fact]
        public void CreatingEmptyStruct_EnumerableIsEmpty()
        {
            var mb = new Maybe<int>();

            mb.Should().BeEmpty();
        }

        [Fact]
        public void CreatingWithDefaultValueType_EnumerableIsNotEmptyAndContainsValue()
        {
            var mb = new Maybe<int>(default(int));

            mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(default(int));
        }

        [Fact]
        public void CreatingWithNull_IsEmpty()
        {
            var mb = new Maybe<string>(null);

            mb.Should().BeEmpty();
        }

        [Fact]
        public void CreatingWithNullableNull_IsEmpty()
        {
            int? value = null;
            var mb = new Maybe<int?>(value);

            mb.Should().BeEmpty();
        }

        [Fact]
        public void CreatingWithNullableValue_IsNotEmpty()
        {
            int? value = 10;

            var mb = new Maybe<int?>(value);

            mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(value);
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        public void CreatingWithValue_ContainsValue<T>(T value)
        {
            var mb = new Maybe<T>(value);

            mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(value);
        }
    }
}