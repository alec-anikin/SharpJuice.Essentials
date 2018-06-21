using System.Collections.Generic;

namespace SharpJuice.Essentials
{
    public static class DictionaryExtensions
    {
        public static Maybe<TValue> GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue value) 
                ? value.ToMaybe() 
                : new Maybe<TValue>();
        }
    }
}
