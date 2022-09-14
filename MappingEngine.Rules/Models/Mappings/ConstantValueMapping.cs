using MappingEngine.Rules.Interface;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

public class ConstantValueMapping : Mapping
{
    public override string Type => "Constant value";

    public virtual object Value { get; set; } = null;

    [JsonIgnore]
    protected override bool IsDerivedConfigured => true;  // Note: null is a legit value

    public override async Task<object> GetOutputAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        return await Task.Run(() => Value);  // async cheater
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
