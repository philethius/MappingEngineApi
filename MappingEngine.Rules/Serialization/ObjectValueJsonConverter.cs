using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MappingEngine.Rules.Serialization
{
    public class ObjectValueJsonConverter : JsonConverter<object>
    {
        public override object Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.True) return true;
            if (reader.TokenType == JsonTokenType.False) return false;
            if (reader.TokenType == JsonTokenType.String) return reader.GetString();
            if (reader.TokenType == JsonTokenType.Number)
            {
                var isSuccess = reader.TryGetInt32(out var i);
                if (isSuccess) return i;

                isSuccess = reader.TryGetInt64(out var lng);
                if (isSuccess) return lng;

                isSuccess = reader.TryGetDecimal(out var dec);
                if (isSuccess) return dec;
            }

            var converter = 
                options.GetConverter(typeof(JsonElement)) as JsonConverter<JsonElement>;
            if (converter != null) return converter.Read(ref reader, typeToConvert, options);

            throw new JsonException("Unable to convert JSON value.");
        }

        public override void Write(
            Utf8JsonWriter writer, 
            object value, 
            JsonSerializerOptions options)
        {
            if (value is bool) { writer.WriteBooleanValue((bool)value); return; }
            if (value is string) { writer.WriteStringValue((string)value); return; }
            if (value is int) { writer.WriteNumberValue((int)value); return; }
            if (value is long) { writer.WriteNumberValue((long)value); return; }
            if (value is decimal) { writer.WriteNumberValue((decimal)value); return; }

            throw new JsonException($"Unable to write value: {value} as a JSON value.");
        }
    }
}
