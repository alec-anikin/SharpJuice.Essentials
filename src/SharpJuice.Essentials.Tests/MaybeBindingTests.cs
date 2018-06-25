using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeBindingTests
    {
        [Theory]
        [InlineData(default(object), 0)]
        [InlineData(10, 1)]
        [InlineData("Test", 1)]
        public void BindingAction_ActionIsCalledForValue<T>(T value, int callsCount)
        {
            var mb = new Maybe<T>(value);
            int calls = 0;

            Action<T> action = s => ++calls;

            mb.Bind(action);

            calls.Should().Be(callsCount);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData("Test", 1)]
        [InlineData(null, 0)]
        public async Task BindingAsyncAction_ActionIsCalledForValue<T>(T value, int callsCount)
        {
            var mb = new Maybe<T>(value);
            int calls = 0;

            Func<T, Task> action = s =>
            {
                ++calls;
                return Task.CompletedTask;
            };

            await mb.Bind(action);

            calls.Should().Be(callsCount);
        }

        [Theory]
        [InlineData(default(object), 0)]
        [InlineData(10, 1)]
        [InlineData("Test", 1)]
        public void BindingFunc_FuncIsCalledForValue<T>(T value, int callsCount)
        {
            var mb = new Maybe<T>(value);
            int calls = 0;

            Func<T, int> binder = s => ++calls;

            mb.Bind(binder);

            calls.Should().Be(callsCount);
        }

        [Theory]
        [InlineData(default(object), 0)]
        [InlineData(10, 1)]
        [InlineData("Test", 1)]
        public async Task BindingAsyncFunc_FuncIsCalledForValue<T>(T value, int callsCount)
        {
            var mb = new Maybe<T>(value);
            int calls = 0;

            Func<T, Task<int>> binder = s =>
            {
                ++calls;
                return Task.FromResult(4);
            };

            await mb.Bind(binder);

            calls.Should().Be(callsCount);
        }

        [Fact]
        public void BindFuncWithEmpty_ResultIsEmpty()
        {
            var mb = new Maybe<object>();

            var result = mb.Bind(i => i.GetType());

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task BindingAsyncFuncWithEmpty_ResultIsEmpty()
        {
            var mb = new Maybe<object>();

            var result = await mb.Bind(i => Task.FromResult(i.GetType()));

            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        public void BindingFuncWithNotEmpty_ResultIsNotEmpty<T>(T value)
        {
            var mb = new Maybe<T>(value);

            var result = mb.Bind(i => i.GetType());

            result.Should().NotBeEmpty().And.ContainSingle(type => type == typeof(T));
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        public async Task BindingAsyncFuncWithNotEmpty_ResultIsNotEmpty<T>(T value)
        {
            var mb = new Maybe<T>(value);

            var result = await mb.Bind(i => Task.FromResult(i.GetType()));

            result.Should().NotBeEmpty().And.ContainSingle(type => type == typeof(T));
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        [InlineData(null)]
        public void BindingFuncWhichReturnsNull_ResultIsEmpty<T>(T value)
        {
            var mb = new Maybe<T>(value);

            var result = mb.Bind(i => (string) null);

            result.Should().BeEmpty();
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        public void BindingFuncWhichReturnsMaybe_ResultIsFlatten<T>(T value)
        {
            var mb = new Maybe<T>(value);

            var result = mb.Bind(i => i.GetType().ToMaybe());

            result.Should().NotBeEmpty().And.ContainSingle(type => type == typeof(T));
        }

        [Theory]
        [InlineData(10)]
        [InlineData("Test")]
        public async Task BindingAsyncFuncWhichReturnsMaybe_ResultIsFlatten<T>(T value)
        {
            var mb = new Maybe<T>(value);

            var result = await mb.Bind(i => Task.FromResult(i.GetType().ToMaybe()));

            result.Should().NotBeEmpty().And.ContainSingle(type => type == typeof(T));
        }
    }
}