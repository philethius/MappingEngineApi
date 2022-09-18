using MappingEngine.Rules.Interface;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

public class ConstantValueMapping : Mapping
{
    private object _value;

    public override string Type => "Constant value";

    public virtual object Value
    {
        get => _value;
        set { _value = value; DataType = value?.GetType() ?? null; }
    }

    [JsonIgnore]
    protected override bool IsDerivedConfigured => true;  // Note: null is a legit value

    public ConstantValueMapping()
    {
        Value = null;
    }

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
