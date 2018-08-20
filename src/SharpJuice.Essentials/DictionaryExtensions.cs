using System.Collections.Generic;

namespace SharpJuice.Essentials
{
    public static class DictionaryExtensions
    {
        public static Maybe<TValue> GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value)
                ? value
                : new Maybe<TValue>();
        }

        public static Maybe<TValue> Value<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value)
                ? value
                : new Maybe<TValue>();
        }
    }
}