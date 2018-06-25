using System;
using System.Threading.Tasks;

namespace SharpJuice.Essentials
{
    public static class ExceptionExtension
    {
        public static T OrThrow<T>(this Maybe<T> maybe, Exception ex)
        {
            if (!maybe.Any())
                throw ex;

            return maybe.Single();
        }

        public static T OrThrow<T>(this Maybe<T> maybe, Func<Exception> exceptionFactory)
        {
            if (!maybe.Any())
                throw exceptionFactory();

            return maybe.Single();
        }

        public static async Task<T> OrThrow<T>(this Task<Maybe<T>> maybeTask, Exception ex)
        {
            if (!(await maybeTask).Any())
                throw ex;

            return maybeTask.Result.Single();
        }

        public static async Task<T> OrThrow<T>(this Task<Maybe<T>> maybeTask, Func<Exception> exceptionFactory)
        {
            if (!(await maybeTask).Any())
                throw exceptionFactory();

            return maybeTask.Result.Single();
        }

        //The reason of the methods BindThrow* below is incorrect Bind overload choosing for lambda i => throw new Ex()
        //https://stackoverflow.com/questions/23486452/lambda-of-x-throw-inferred-to-match-funct-task-in-overloaded-metho

        public static Maybe<T> BindThrow<T>(this Maybe<T> maybe, Exception ex)
        {
            if (maybe.Any())
                throw ex;

            return maybe;
        }

        public static Maybe<T> BindThrow<T>(this Maybe<T> maybe, Func<Exception> exceptionFactory)
        {
            if (maybe.Any())
                throw exceptionFactory();

            return maybe;
        }

        public static async Task<Maybe<T>> BindThrow<T>(this Task<Maybe<T>> maybeTask, Exception ex)
        {
            if ((await maybeTask).Any())
                throw ex;

            return maybeTask.Result;
        }

        public static async Task<Maybe<T>> BindThrow<T>(this Task<Maybe<T>> maybeTask, Func<Exception> exceptionFactory)
        {
            if ((await maybeTask).Any())
                throw exceptionFactory();

            return maybeTask.Result;
        }
    }
}