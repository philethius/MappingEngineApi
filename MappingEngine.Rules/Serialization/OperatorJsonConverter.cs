using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Models.Operators;
using MappingEngine.Rules.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MappingEngine.Rules.Serialization
{
    public class OperatorJsonConverter : JsonConverter<Operator>
    {
        private static IDictionary<string, Type> AvailableOperators =
            ReflectionHelper.GetAvailableOf<Operator>();

        public override Operator Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var internalReader = reader;
            if (internalReader.TokenType != JsonTokenType.StartObject) throw new JsonException();

            string propertyName = null;
            do
            {
                if (internalReader.TokenType != JsonTokenType.PropertyName) { continue; }

                propertyName = internalReader.GetString();
                if (propertyName == "Type")
                {
                    break;
                }
            }
            while (internalReader.Read() && internalReader.TokenType != JsonTokenType.EndObject);

            if (propertyName == null) { throw new JsonException(); }

            internalReader.Read();
            if (internalReader.TokenType != JsonTokenType.String) throw new JsonException();

            var type = internalReader.GetString();

            var oper = (Operator)JsonSerializer.Deserialize(ref reader, AvailableOperators[type], options);

            return oper;
        }

        public override void Write(
            Utf8JsonWriter writer,
            Operator value,
            JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToJson(options));
        }
    }
}
