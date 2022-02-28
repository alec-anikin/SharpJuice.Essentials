using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class JsonTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void SerializeJson_Class()
        {
            var value = new Maybe<TestClass>(_fixture.Create<TestClass>());

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<Maybe<TestClass>>(json);

            deserialized.Should().NotBeEmpty();
            deserialized.Single().Should().BeEquivalentTo(value.Single());
        }

        [Fact]
        public void SerializeJson_Struct()
        {
            var value = new Maybe<TestStruct>(_fixture.Create<TestStruct>());

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<Maybe<TestStruct>>(json);

            deserialized.Should().NotBeEmpty();
            deserialized.Single().Should().BeEquivalentTo(value.Single());
        }

        [Fact]
        public void SerializeJson_EmptyClass()
        {
            var value = new Maybe<TestClass>();

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<Maybe<TestClass>>(json);

            deserialized.Should().BeEmpty();
            deserialized.Any().Should().BeFalse();
        }

        [Fact]
        public void SerializeJson_EmptyStruct()
        {
            var value = new Maybe<TestStruct>();

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<Maybe<TestStruct>>(json);

            deserialized.Should().BeEmpty();
            deserialized.Any().Should().BeFalse();
        }

        [Fact]
        public void SerializeJson_ClassWithMaybe()
        {
            var value = _fixture.Create<OuterClass>();

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<OuterClass>(json);

            deserialized.Should().BeEquivalentTo(value);
        }

        [Fact]
        public void SerializeJson_ClassWithEmptyMaybe()
        {
            var value = new OuterClass(default, default);

            var json = JsonSerializer.Serialize(value);

            var deserialized = JsonSerializer.Deserialize<OuterClass>(json);

            deserialized.Should().BeEquivalentTo(value);
            deserialized.maybe.Any().Should().BeFalse();
        }

        private sealed record OuterClass(Maybe<TestClass> maybe, int other);

        private sealed record TestClass(int valueInt, string valueString, DateTime valueDateTime,
            InnerClass valueClass);

        private sealed record InnerClass(Guid guid);

        private readonly struct TestStruct
        {
            [JsonConstructor]
            public TestStruct(int x, int y, string value)
            {
                X = x;
                Y = y;
                Value = value;
            }

            public int X { get; }

            public int Y { get; }

            public string Value { get; }
        }
    }
}