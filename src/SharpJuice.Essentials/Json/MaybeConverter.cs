using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharpJuice.Essentials.Json
{
    public sealed class MaybeConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
                return false;

            return typeToConvert.GetGenericTypeDefinition() == typeof(Maybe<>);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            var valueType = type.GetGenericArguments()[0];

            var converter = (JsonConverter)Activator.CreateInstance(
                typeof(MaybeConverterInner<>).MakeGenericType(valueType),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class MaybeConverterInner<TValue> : JsonConverter<Maybe<TValue>>
        {
            private readonly JsonConverter<TValue> _valueConverter;
            private readonly Type _valueType;

            public MaybeConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));
                _valueType = typeof(TValue);
            }

            public override Maybe<TValue> Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    return default;

                return _valueConverter != null
                    ? _valueConverter.Read(ref reader, _valueType, options)
                    : JsonSerializer.Deserialize<TValue>(ref reader, options);
            }

            public override void Write(Utf8JsonWriter writer, Maybe<TValue> maybe, JsonSerializerOptions options)
            {
                if (!maybe.Any())
                {
                    writer.WriteNullValue();
                    return;
                }

                if (_valueConverter != null)
                    _valueConverter.Write(writer, maybe.Single(), options);
                else
                    JsonSerializer.Serialize(writer, maybe.Single(), options);
            }
        }
    }
}