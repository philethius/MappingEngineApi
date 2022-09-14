using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Models.Operators;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MappingEngine.Rules.Models;

public class Rule
{
    public Mapping[] IfTrueMappings { get; set; }
    public Mapping[] IfFalseMappings { get; set; }
    public Operator Operator { get; set; }

    [JsonIgnore]
    public bool IsConfigured => Operator != null
        && (
            (IfTrueMappings != null && IfTrueMappings.Length > 0)
            || (IfFalseMappings != null && IfFalseMappings.Length > 0)
        );

    public string GetSerializedRule(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
