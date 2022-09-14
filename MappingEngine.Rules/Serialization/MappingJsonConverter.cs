using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MappingEngine.Rules.Serialization;

public class MappingJsonConverter : JsonConverter<Mapping>
{
    private static IDictionary<string, Type> AvailableMappings =
        ReflectionHelper.GetAvailableOf<Mapping>();

    public override Mapping Read(
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

        var mapping = (Mapping)JsonSerializer.Deserialize(ref reader, AvailableMappings[type], options);

        return mapping;
    }
    
    public override void Write(
        Utf8JsonWriter writer, 
        Mapping value, 
        JsonSerializerOptions options)
    {
        writer.WriteRawValue(value.ToJson(options));
    }
}
