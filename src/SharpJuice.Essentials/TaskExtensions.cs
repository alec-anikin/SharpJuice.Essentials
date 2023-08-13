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
            Func<T, Task<TResult>> binder)
        {
            return await (await task).Bind(binder);
        }

        public static async Task<Maybe<T>> Filter<T>(
            this Task<Maybe<T>> task,
            Predicate<T> predicate)
        {
            return (await task).Filter(predicate);
        }

        public static async Task<Maybe<T>> Filter<T>(
            this Task<Maybe<T>> task,
            Func<T, Task<bool>> predicate)
        {
            return await (await task).Filter(predicate);
        }

        public static async Task<T> OrElse<T>(this Task<Maybe<T>> task, Func<T> func)
        {
            return (await task).OrElse(func);
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

        public static async Task Match<T>(this Task<Maybe<T>> task, Action<T> bind, Action orElse)
        {
            (await task).Match(bind, orElse);
        }

        public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, TResult> bind,
            Func<TResult> orElse)
        {
            return (await task).Match(bind, orElse);
        }

        public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, TResult> bind,
            TResult orElse)
        {
            return (await task).Match(bind, orElse);
        }

        public static async Task<TResult> Match<T, TResult>(this Task<Maybe<T>> task, Func<T, Task<TResult>> bind,
            Func<Task<TResult>> orElse)
        {
            return await (await task).Match(bind, orElse);
        }

        public static async Task<Maybe<T>> Flat<T>(this Task<Maybe<Maybe<T>>> task)
        {
            return await task;
        }
    }
}