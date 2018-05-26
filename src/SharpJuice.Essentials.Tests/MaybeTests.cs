using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public class MaybeTests
	{
		[Theory]
		[InlineData(default(int))]
		[InlineData(default(object))]
		public void Maybe_WhenEmpty<T>(T stub)
		{
			var mb = new Maybe<T>();
			mb.Should().BeEmpty();
			mb.GetEnumerator().Should().NotBeSameAs(mb.GetEnumerator());
		}

		[Fact]
		public void Maybe_WhenDefaultValue_ValueType()
		{
			var mb = new Maybe<int>(default(int));
			mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(default(int));			
		}

		[Fact]
		public void Maybe_WhenNull_ReferenceType()
		{
			var mb = new Maybe<string>(null);

			mb.Should().BeEmpty();			
		}

		[Fact]
		public void Maybe_WhenNullableNull_MaybeIsEmpty()
		{
			int? value = null;
			var mb = new Maybe<int?>(value);

			mb.Should().BeEmpty();
		}

		[Fact]
		public void Maybe_WhenNullableHaveValue_MaybeIsNotEmpty()
		{
			int? value = 10;

			var mb = new Maybe<int?>(value);

			mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(value);
		}

		[Theory]
		[InlineData(10)]
		[InlineData("Test")]
		public void Maybe_WhenNonDefaultValue<T>(T value)
		{
			var mb = new Maybe<T>(value);

			mb.Should().NotBeEmpty().And.HaveCount(1).And.Contain(value);			
		}

		[Theory]
		[InlineData(default(int))]
		[InlineData(default(object))]
		public void MaybeBindAction_WhenEmpty_ActionIsNotCalled<T>(T stub)
		{
			var mb = new Maybe<T>();
			int calls = 0;

			Action<T> action = s => ++calls;

			mb.Bind(action);

			calls.Should().Be(0);
		}


		[Theory]
		[InlineData(10)]
		[InlineData("Test")]
		public void MaybeBindAction_WhenNotEmpty_ActionIsCalled<T>(T value)
		{
			var mb = new Maybe<T>(value);
			int calls = 0;

			Action<T> action = s => ++calls;

			mb.Bind(action);

			calls.Should().Be(1);
		}

		[Theory]
		[InlineData(default(int))]
		[InlineData(default(object))]
		public void MaybeBindFunc_WhenEmpty_FuncIsNotCalled<T>(T stub)
		{
			var mb = new Maybe<T>();
			int calls = 0;

			mb.Bind(s => ++calls);

			calls.Should().Be(0);
		}

		[Theory]
		[InlineData(default(int))]
		[InlineData(default(object))]
		public void MaybeBindFunc_WhenEmpty_ResultIsEmpty<T>(T stub)
		{
			var mb = new Maybe<T>();
			
			var result = mb.Bind(i => i.GetType());

			result.Should().BeEmpty();
		}

		[Theory]
		[InlineData(10)]
		[InlineData("Test")]
		public void MaybeBindFunc_WhenNotEmpty_ResultIsNotEmpty<T>(T value)
		{
			var mb = new Maybe<T>(value);

			var result = mb.Bind(i => i.GetType());

			result.Should().NotBeEmpty().And.ContainSingle(type => type == typeof(T));
		}

		[Fact]
		public void MaybeIsNotEmpty_IfEmptyDoesNotReplaceValue()
		{
			var maybe = new Maybe<int>(25);
			var maybe2 = maybe.IfEmpty(() => 10.ToMaybe());

			maybe2.Should().OnlyContain(v => v == 25);
		}

		[Fact]
		public void MaybeIsEmpty_IfEmptyReturnsMaybeWithValue()
		{
			var maybe = new Maybe<int>();
			var maybe2 = maybe.IfEmpty(() => 10.ToMaybe());

			maybe2.Should().OnlyContain(v => v == 10);
		}

		[Fact]
		public async Task MaybeIsNotEmpty_IfEmptyAsyncDoesNotReplaceValue()
		{
			var maybe = new Maybe<int>(25);
			var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10.ToMaybe()));

			maybe2.Should().OnlyContain(v => v == 25);
		}

		[Fact]
		public async Task MaybeIsEmpty_IfEmptyAsyncReturnsMaybeWithValue()
		{
			var maybe = new Maybe<int>();
			var maybe2 = await maybe.IfEmpty(async () => await Task.FromResult(10.ToMaybe()));

			maybe2.Should().OnlyContain(v => v == 10);
		}
	}
}
