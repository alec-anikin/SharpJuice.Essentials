using System;
using System.Threading.Tasks;

namespace SharpJuice.Essentials
{
    public static class TaskExtensions
    {
        public static async Task Bind<T>(this Task<Maybe<T>> task, Action<T> action)
        {
            (await task).Bind(action);
        }

        public static async Task Bind<T>(this Task<Maybe<T>> task, Func<T, Task> binder)
        {
            await (await task).Bind(binder);
        }

        public static async Task<Maybe<TResult>> Bind<T, TResult>(
            this Task<Maybe<T>> task,
            Func<T, TResult> binder)
        {
            return (await task).Bind(binder);
        }

        public static async Task<Maybe<TResult>> Bind<T, TResult>(
            this Task<Maybe<T>> task,
            Func<T, Maybe<TResult>> binder)
        {
            return (await task).Bind(binder);
        }

        public static async Task<Maybe<TResult>> Bind<T, TResult>(
            this Task<Maybe<T>> task,
            Func<T, Task<TResult>> binder)
        {
            return await (await task).Bind(binder);
        }

        public static async Task<Maybe<TResult>> Bind<T, TResult>(
            this Task<Maybe<T>> task,
            Func<T, Task<Maybe<TResult>>> binder)
        {
            return await (await task).Bind(binder);
        }

        public static async Task<Maybe<T>> OrElse<T>(this Task<Maybe<T>> task, Func<Maybe<T>> func)
        {
            return (await task).OrElse(func);
        }

        public static async Task<T> OrElse<T>(this Task<Maybe<T>> task, Func<T> func)
        {
            return (await task).OrElse(func);
        }

        public static async Task<Maybe<T>> OrElse<T>(this Task<Maybe<T>> task, Func<Task<Maybe<T>>> func)
        {
            return await (await task).OrElse(func);
        }

        public static async Task<T> OrElse<T>(this Task<Maybe<T>> task, Func<Task<T>> func)
        {
            return await (await task).OrElse(func);
        }

        public static async Task<T> OrElse<T>(this Task<Maybe<T>> task, T value)
        {
            return (await task).OrElse(value);
        }

        public static async Task<T> OrDefault<T>(this Task<Maybe<T>> task)
        {
            return (await task).OrDefault();
        }

        public static async Task<bool> Any<T>(this Task<Maybe<T>> task)
        {
            return (await task).Any();
        }

        public static async Task<T> Single<T>(this Task<Maybe<T>> task)
        {
            return (await task).Single();
        }
    }
}