using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class MaybeMatchingTests
    {
        private int _bindCalls = 0;
        private int _orElseCalls = 0;

        [Fact]
        public void MatchingEmpty_OrElseCalled()
        {
            var mb = new Maybe<int>();
            mb.Match(
                v => { _bindCalls++; },
                () => { _orElseCalls++; });

            _bindCalls.Should().Be(0);
            _orElseCalls.Should().Be(1);
        }

        [Fact]
        public void MatchingValue_BindCalled()
        {
            var mb = new Maybe<int>(100);

            mb.Match(
                v => { _bindCalls++; },
                () => { _orElseCalls++; });

            _bindCalls.Should().Be(1);
            _orElseCalls.Should().Be(0);
        }

        [Fact]
        public void MatchingEmpty_OrElseWithResultCalled()
        {
            const int orElse = 100500;
            var mb = new Maybe<int>();
            var result = mb.Match(
                v => _bindCalls++,
                () =>
                {
                    _orElseCalls++;
                    return orElse;
                });

            _bindCalls.Should().Be(0);
            _orElseCalls.Should().Be(1);
            result.Should().Be(orElse);
        }

        [Fact]
        public void MatchingValue_BindWithResultCalled()
        {
            const int bind = 100500;
            var mb = new Maybe<int>(100);

            var result = mb.Match(v =>
                {
                    _bindCalls++;
                    return bind;
                },
                () => _orElseCalls++);

            _bindCalls.Should().Be(1);
            _orElseCalls.Should().Be(0);
            result.Should().Be(bind);
        }

        [Fact]
        public void MatchingEmpty_OrElseWithConstResultCalled()
        {
            const int orElse = 100500;
            var mb = new Maybe<int>();
            var result = mb.Match(v => 0, orElse);

            result.Should().Be(orElse);
        }

        [Fact]
        public void MatchingValue_BindWithConstResultCalled()
        {
            const int bind = 100500;
            var mb = new Maybe<int>(100);

            var result = mb.Match(v => bind, 25);

            result.Should().Be(bind);
        }
    }
}