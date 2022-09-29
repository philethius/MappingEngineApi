using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

/// <summary>
/// Represents a value mapping that comes from an interactive behavior
/// </summary>
public class UserInputValueMapping : ConstantValueMapping
{
    public override string Type => "Input value";

    /// <summary>
    /// Optional; sets the prompted value for input
    /// </summary>
    public string Prompt { get; set; }

    /// <summary>
    /// Required.  Designates the identifier for this variable.  Must be unique.
    /// </summary>
    public string Identifier { get; set; }

    protected override bool IsDerivedConfigured => !string.IsNullOrWhiteSpace(Identifier);

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
