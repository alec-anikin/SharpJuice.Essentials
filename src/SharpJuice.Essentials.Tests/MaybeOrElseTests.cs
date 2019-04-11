using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeOrElseTests
    {
        [Fact]
        public void ReplacingNotEmptyValueReturningMaybe_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = maybe.OrElse(() => 10.ToMaybe());

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public void ReplacingEmptyValueReturningMaybe_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = maybe.OrElse(() => 10.ToMaybe());

            maybe2.Should().OnlyContain(v => v == 10);
        }

        [Fact]
        public void ReplacingNotEmptyValue_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var value = maybe.OrElse(() => 10);

            value.Should().Be(25);
        }

        [Fact]
        public void ReplacingEmptyValue_ReturnsValue()
        {
            var maybe = new Maybe<int>();
            var value = maybe.OrElse(() => 10);

            value.Should().Be(10);;
        }

        [Fact]
        public void ReplacingEmptyValueWithDefault_ReturnsDefault()
        {
            var maybe = new Maybe<int>();
            var value = maybe.OrDefault();

            value.Should().Be(0);;
        }

        [Fact]
        public async Task ReplacingNotEmptyValueAsyncReturningMaybe_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = await maybe.OrElse(async () => await Task.FromResult(10.ToMaybe()));

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public async Task ReplacingEmptyValueAsyncReturningMaybe_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = await maybe.OrElse(async () => await Task.FromResult(10.ToMaybe()));

            maybe2.Should().OnlyContain(v => v == 10);
        }

        [Fact]
        public async Task ReplacingNotEmptyValueAsync_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var value = await maybe.OrElse(async () => await Task.FromResult(10));

            value.Should().Be(25);
        }

        [Fact]
        public async Task ReplacingEmptyValueAsync_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var value = await maybe.OrElse(async () => await Task.FromResult(10));

            value.Should().Be(10);
        }

        [Fact]
        public void ReplacingEmptyValueWithEmptyValue_ReturnsEmpty()
        {
            var a = new Maybe<int>();
            var b = new Maybe<int>();
            var value = a.OrElse(b);

            value.Should().BeEmpty();
        }

        [Fact]
        public void ChoosingEmptyValueWithValue_ReturnsValue()
        {
            var a = new Maybe<int>();
            var b = new Maybe<int>(10);
            var value = a.OrElse(b);

            value.Should().Contain(10);
        }

        [Fact]
        public void ChoosingValueOrEmptyValue_ReturnsValue()
        {
            var a = new Maybe<int>(10);
            var b = new Maybe<int>();
            var value = a.OrElse(b);

            value.Should().Contain(10);
        }

        [Fact]
        public void ChoosingValue1OrValue2_ReturnsValue1()
        {
            var a = new Maybe<int>(10);
            var b = new Maybe<int>(20);
            var value = a.OrElse(b);

            value.Should().Contain(10);
        }
    }
}