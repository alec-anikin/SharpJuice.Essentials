using System;

namespace SharpJuice.Essentials
{
	public static class StringExtensions
	{
		public static string Truncate(this string value, int maxLength)
		{
			return string.IsNullOrEmpty(value) || value.Length <= maxLength 
				? value 
				: value.Substring(0, maxLength);
		}

		public static T ToEnum<T>(this string value) where T : struct
		{
			return (T)Enum.Parse(typeof(T), value);
		}
	}
}