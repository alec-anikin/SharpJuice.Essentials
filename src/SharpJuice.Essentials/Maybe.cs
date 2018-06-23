using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SharpJuice.Essentials
{
    /// <summary>
    /// Replacement for null. Contains one or zero items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public struct Maybe<T> :
        IEnumerable<T>,
        ISerializable,
        IEquatable<Maybe<T>>,
        IEquatable<T>
    {
        private readonly Enumerator _enumerator;

        public Maybe(T value)
        {
            _enumerator = value == null
                ? new Enumerator()
                : new Enumerator(value);
        }

        private Maybe(SerializationInfo info, StreamingContext context)
            : this((T) info.GetValue("Value", typeof(T)))
        {
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", _enumerator.Item, typeof(T));
        }

        public void Bind(Action<T> action)
        {
            if (_enumerator.HasItem) action(_enumerator.Item);
        }

        public Task Bind(Func<T, Task> binder)
        {
            return _enumerator.HasItem
                ? binder(_enumerator.Item)
                : Task.CompletedTask;
        }

        public Maybe<TResult> Bind<TResult>(Func<T, TResult> binder)
        {
            return _enumerator.HasItem
                ? new Maybe<TResult>(binder(_enumerator.Item))
                : new Maybe<TResult>();
        }

        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> binder)
        {
            return _enumerator.HasItem
                ? binder(_enumerator.Item)
                : new Maybe<TResult>();
        }

        public async Task<Maybe<TResult>> Bind<TResult>(Func<T, Task<TResult>> binder)
        {
            return _enumerator.HasItem
                ? new Maybe<TResult>(await binder(_enumerator.Item))
                : new Maybe<TResult>();
        }

        public Task<Maybe<TResult>> Bind<TResult>(Func<T, Task<Maybe<TResult>>> binder)
        {
            return _enumerator.HasItem
                ? binder(_enumerator.Item)
                : Task.FromResult(new Maybe<TResult>());
        }

        public Maybe<T> IfEmpty(Func<Maybe<T>> func) => !_enumerator.HasItem ? func() : this;

        public Maybe<T> IfEmpty(Func<T> func) => !_enumerator.HasItem ? new Maybe<T>(func()) : this;

        public Task<Maybe<T>> IfEmpty(Func<Task<Maybe<T>>> func)
        {
            return !_enumerator.HasItem ? func() : Task.FromResult(this);
        }

        public async Task<Maybe<T>> IfEmpty(Func<Task<T>> func)
        {
            return !_enumerator.HasItem ? new Maybe<T>(await func()) : this;
        }

        public bool Any() => _enumerator.HasItem;

        public T Single()
        {
            if (_enumerator.HasItem)
                return _enumerator.Item;

            throw new InvalidOperationException("Maybe has no item");
        }

        public T DefaultIfEmpty(T defaultValue = default(T))
        {
            return _enumerator.HasItem ? _enumerator.Item : defaultValue;
        }

        public bool Equals(Maybe<T> other)
        {
            if (_enumerator.HasItem != other._enumerator.HasItem)
                return false;

            return !_enumerator.HasItem || _enumerator.Item.Equals(other._enumerator.Item);
        }

        public bool Equals(T other) => _enumerator.HasItem && _enumerator.Item.Equals(other);

        public IEnumerator<T> GetEnumerator() => _enumerator;

        IEnumerator IEnumerable.GetEnumerator() => _enumerator;

        private struct Enumerator : IEnumerator<T>
        {
            public readonly T Item;
            public readonly bool HasItem;
            private bool _moved;

            public Enumerator(T item)
            {
                Item = item;
                HasItem = true;
                _moved = false;
            }

            public T Current => Item;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (!HasItem || _moved)
                {
                    return false;
                }

                _moved = true;
                return true;
            }

            void IEnumerator.Reset() => _moved = false;
        }
    }
}