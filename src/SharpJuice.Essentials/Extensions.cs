namespace SharpJuice.Essentials
{
    public static class Extensions
    {
        public static Maybe<T> ToMaybe<T>(this T item)
        {
            return new Maybe<T>(item);
        }

        public static Maybe<string> ToMaybe(this string item, bool emptyAsNull)
        {
            if (emptyAsNull && string.IsNullOrWhiteSpace(item))
                return new Maybe<string>();

            return new Maybe<string>(item);
        }

        public static Maybe<T> ToMaybe<T>(this T? item) where T : struct
        {
            return item.HasValue ? new Maybe<T>(item.Value) : new Maybe<T>();
        }

        public static Maybe<TTo> As<TTo>(this object item) where TTo : class
        {
            return new Maybe<TTo>(item as TTo);
        }

        public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct
        {
            return maybe.Any() ? maybe.Single() : default(T?);
        }
    }
}