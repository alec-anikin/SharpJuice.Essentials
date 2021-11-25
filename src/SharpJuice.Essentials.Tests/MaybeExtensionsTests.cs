using System;
using System.Linq;
using FluentAssertions;
using SharpJuice.Essentials;
using Xunit;

namespace MaybeLinq.Tests
{
    public sealed class MaybeExtensionsTests
    {
        [Fact]
        public void Primitive_EmptySource_EmptyResult()
        {
            var collection = Array.Empty<Maybe<int>>();

            var results = collection.SelectMany(i => i);

            results.Should().BeEmpty();
        }

        [Fact]
        public void Primitive_SourceContainsOnlyEmptyValues_EmptyResult()
        {
            var collection = new[]
            {
                new Maybe<int>(),
                new Maybe<int>(),
                new Maybe<int>()
            };

            var results = collection.SelectMany(i => i);

            results.Should().BeEmpty();
        }

        [Fact]
        public void Primitive_SourceContainsValues_AllValuesReturned()
        {
            var collection = new Maybe<int>[] { 1, 2, 3 };

            var results = collection.SelectMany(i => i);

            results.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void Primitive_SourceContainsEmptyValues_NotEmptyValuesReturned()
        {
            var collection = new Maybe<int>[] { 1, new(), 3 };

            var results = collection.SelectMany(i => i);

            results.Should().BeEquivalentTo(new[] { 1, 3 });
        }

        [Fact]
        public void Complex_EmptySource_EmptyResult()
        {
            var collection = Array.Empty<Complex>();

            var results = collection.SelectMany(i => i.Value);

            results.Should().BeEmpty();
        }

        [Fact]
        public void Complex_SourceContainsOnlyEmptyValues_EmptyResult()
        {
            var collection = new Complex[]
            {
                new(new Maybe<int>()),
                new(new Maybe<int>()),
                new(new Maybe<int>())
            };

            var results = collection.SelectMany(i => i.Value);

            results.Should().BeEmpty();
        }

        [Fact]
        public void Complex_SourceContainsValues_AllValuesReturned()
        {
            var collection = new Complex[] { new(1), new(2), new(3) };

            var results = collection.SelectMany(i => i.Value);

            results.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void Complex_SourceContainsEmptyValues_NotEmptyValuesReturned()
        {
            var collection = new Complex[] { new(1), new(new Maybe<int>()), new(3) };

            var results = collection.SelectMany(i => i.Value);

            results.Should().BeEquivalentTo(new[] { 1, 3 });
        }

        private sealed class Complex
        {
            public Complex(Maybe<int> value)
                => Value = value;

            public Maybe<int> Value { get; }
        }
    }
}