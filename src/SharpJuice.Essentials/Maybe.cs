using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpJuice.Essentials
{
    /// <summary>
    /// Replacement for null. Contains one or zero items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public readonly struct Maybe<T> :
        IEnumerable<T>,
        IEquatable<Maybe<T>>,
        IEquatable<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        public Maybe(T value)
        {
            if (value != null)
            {
                _hasValue = true;
                _value = value;
            }
            else
            {
                _hasValue = false;
                _value = default;
            }
        }

        public void Bind(Action<T> action)
        {
            if (_hasValue) action(_value);
        }

        public Task Bind(Func<T, Task> binder)
        {
            return _hasValue
                ? binder(_value)
                : Task.CompletedTask;
        }

        public Maybe<TResult> Bind<TResult>(Func<T, TResult> binder)
        {
            return _hasValue
                ? new Maybe<TResult>(binder(_value))
                : new Maybe<TResult>();
        }
        
        public async Task<Maybe<TResult>> Bind<TResult>(Func<T, Task<TResult>> binder)
        {
            return _hasValue
                ? new Maybe<TResult>(await binder(_value))
                : new Maybe<TResult>();
        }

        public T OrElse(Func<T> func) => _hasValue ? _value : func();
        
        public Task<T> OrElse(Func<Task<T>> func)
        {
            return _hasValue ? Task.FromResult(_value) : func();
        }

        public T OrElse(T value)
        {
            return _hasValue ? _value : value;
        }

        public Maybe<T> OrElse(Maybe<T> value)
        {
            return _hasValue ? this : value;
        }

        public T OrDefault()
        {
            return _hasValue ? _value : default;
        }

        public bool Any() => _hasValue;

        public T Single()
        {
            if (_hasValue)
                return _value;

            throw new InvalidOperationException("Maybe has no item");
        }

        public bool Equals(Maybe<T> other)
        {
            if (_hasValue != other._hasValue)
                return false;

            return !_hasValue || _value.Equals(other._value);
        }

        public bool Equals(T other) => _hasValue && _value.Equals(other);

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

        public static implicit operator Maybe<T>(Maybe<Maybe<T>> nested)
        {
            return nested._hasValue ? nested._value : new Maybe<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_hasValue)
                yield return _value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}