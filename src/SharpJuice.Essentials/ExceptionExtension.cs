using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpJuice.Essentials
{
    public static class ExceptionExtension
    {
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> items, Exception ex)
        {
            if (!items.Any())
                throw ex;

            return items;
        }

        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> items, Func<Exception> exceptionFactory)
        {
            if (!items.Any())
                throw exceptionFactory();

            return items;
        }

        public static Maybe<T> ThrowIfEmpty<T>(this Maybe<T> maybe, Exception ex)
        {
            if (!maybe.Any())
                throw ex;

            return maybe;
        }

        public static Maybe<T> ThrowIfEmpty<T>(this Maybe<T> maybe, Func<Exception> exceptionFactory)
        {
            if (!maybe.Any())
                throw exceptionFactory();

            return maybe;
        }

        public static async Task<Maybe<T>> ThrowIfEmpty<T>(this Task<Maybe<T>> maybeTask, Exception ex)
        {
            if (!(await maybeTask).Any())
                throw ex;

            return maybeTask.Result;
        }

        public static async Task<Maybe<T>> ThrowIfEmpty<T>(this Task<Maybe<T>> maybeTask, Func<Exception> exceptionFactory)
        {
            if (!(await maybeTask).Any())
                throw exceptionFactory();

            return maybeTask.Result;
        }
    }
}