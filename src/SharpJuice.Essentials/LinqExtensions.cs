using System.Collections.Generic;
using SharpJuice.Essentials;

namespace System.Linq
{
    public static class MaybeEnumerable
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Maybe<TResult>> selector)
        {
            using var enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var maybe = selector(enumerator.Current);

                if (maybe.Any())
                    yield return maybe.Single();
            }
        }
    }
}
