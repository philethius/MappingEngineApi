using MappingEngine.Rules.Interface;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MappingEngine.Rules.Models.Mappings;

public delegate Task<object> GetFieldValueDelegate(string field);

public abstract class Mapping : ITypeName
{
    public abstract string Type { get; }
    public string MappingField { get; set; }

    [JsonIgnore]
    public bool IsConfigured => IsDerivedConfigured;

    protected abstract bool IsDerivedConfigured { get; }

    public virtual Type DataType { get; set; } = null;

    public abstract Task<object> GetOutputAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    );

    public abstract string ToJson(JsonSerializerOptions options);
}
