using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public class MaybeFilterTests
    {
        [Fact]
        public void WhenNone_PredicateNotInvoked()
        {
            var maybe = default(Maybe<object>);
            int calls = 0;
            Predicate<object> predicate = _ =>
            {
                calls++;
                return true;
            };

            var result = maybe.Filter(predicate);

            result.Should().BeEmpty();
            calls.Should().Be(0);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData(10)]
        [InlineData(typeof(object))]
        public void WhenSome_PredicateTrue_ReturnsSome<T>(T value)
        {
            var maybe = new Maybe<T>(value);
            Predicate<T> predicate = _ => true;

            var result = maybe.Filter(predicate);

            result.Should().Equal(maybe);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData(10)]
        [InlineData(typeof(object))]
        public void WhenSome_PredicateFalse_ReturnsNone<T>(T value)
        {
            var maybe = new Maybe<T>(value);
            Predicate<T> predicate = _ => false;

            var result = maybe.Filter(predicate);

            result.Should().BeEmpty();
        }


        [Fact]
        public async Task WhenNone_AsyncPredicateNotInvoked()
        {
            var maybe = default(Maybe<object>);
            int calls = 0;
            Func<object, Task<bool>> predicate = _ =>
            {
                calls++;
                return Task.FromResult(true);
            };

            var result = await maybe.Filter(predicate);

            result.Should().BeEmpty();
            calls.Should().Be(0);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData(10)]
        [InlineData(typeof(object))]
        public async Task WhenSome_AsyncPredicateTrue_ReturnsSome<T>(T value)
        {
            var maybe = new Maybe<T>(value);
            Func<T, Task<bool>> predicate = _ => Task.FromResult(true);

            var result = await maybe.Filter(predicate);

            result.Should().Equal(maybe);
        }

        [Theory]
        [InlineData("Test")]
        [InlineData(10)]
        [InlineData(typeof(object))]
        public async Task WhenSome_AsyncPredicateFalse_ReturnsNone<T>(T value)
        {
            var maybe = new Maybe<T>(value);
            Func<T, Task<bool>> predicate = _ => Task.FromResult(false);

            var result = await maybe.Filter(predicate);

            result.Should().BeEmpty();
        }
    }
}
