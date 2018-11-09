using Xunit;

namespace SharpJuice.Essentials.Tests
{
    public sealed class AmbiguousOverloadingTests
    {
        [Fact]
        public void TypeUpcasting_NotAmbiguous()
        {
           var maybe = new Maybe<object>();

            var result = maybe.Bind<IA>(o => Method1())
                .OrElse(() => Method2());
        }

        public A1 Method1() => new A1();
        public A2 Method2() => new A2();

        public interface IA { }

        public sealed class A1 : IA { }
        public sealed class A2 : IA { }
    }
}
