using System;

namespace SharpJuice.Essentials
{
    public static class EnumExtensions
    {
        public static Maybe<TEnum> ToEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct
        {
            return Enum.TryParse(value, ignoreCase, out TEnum result)
                ? result
                : new Maybe<TEnum>();
        }
    }
}