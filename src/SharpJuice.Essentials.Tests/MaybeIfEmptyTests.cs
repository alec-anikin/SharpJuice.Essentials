using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeIfEmptyTests
    {
        [Fact]
        public void ReplacingNotEmptyValueReturningMaybe_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = maybe.IfEmpty(() => 10.ToMaybe());

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public void ReplacingEmptyValueReturningMaybe_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = maybe.IfEmpty(() => 10.ToMaybe());

            maybe2.Should().OnlyContain(v => v == 10);
        }

        [Fact]
        public void ReplacingNotEmptyValue_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = maybe.IfEmpty(() => 10);

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public void ReplacingEmptyValue_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = maybe.IfEmpty(() => 10);

            maybe2.Should().OnlyContain(v => v == 10);
        }

        [Fact]
        public async Task ReplacingNotEmptyValueAsyncReturningMaybe_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10.ToMaybe()));

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public async Task ReplacingEmptyValueAsyncReturningMaybe_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10.ToMaybe()));

            maybe2.Should().OnlyContain(v => v == 10);
        }

        [Fact]
        public async Task ReplacingNotEmptyValueAsync_ValueIsNotReplaced()
        {
            var maybe = new Maybe<int>(25);
            var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10));

            maybe2.Should().OnlyContain(v => v == 25);
        }

        [Fact]
        public async Task ReplacingEmptyValueAsync_ReturnsMaybeWithValue()
        {
            var maybe = new Maybe<int>();
            var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10));

            maybe2.Should().OnlyContain(v => v == 10);
        }
    }
}