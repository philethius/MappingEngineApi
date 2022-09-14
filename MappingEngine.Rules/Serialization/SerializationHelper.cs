using MappingEngine.Rules.Models;
using System.Text.Json;

namespace MappingEngine.Rules.Serialization;

public static class SerializationHelper
{
    public static Rule GetRuleFromJson(string jsonRule)
    {
        return JsonSerializer.Deserialize<Rule>(jsonRule, GetMappingEngineSerializerOptions());
    }

    public static JsonSerializerOptions GetMappingEngineSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            Converters =
            {
                new MappingJsonConverter(),
                new OperatorJsonConverter(),
                new ObjectValueJsonConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
