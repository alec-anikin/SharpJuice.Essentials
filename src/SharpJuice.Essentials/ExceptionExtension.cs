using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpJuice.Essentials
{
    public static class ExceptionExtensionb
    {
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> items, Exception ex)
        {
            if (!items.Any())
            {
                throw ex;
            }

            return items;
        }
    }
}