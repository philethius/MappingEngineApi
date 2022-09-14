using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Mappings;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MappingEngine.Rules.Models.Operators;

public abstract class Operator : ITypeName
{
    public abstract string Type { get; }

    [JsonIgnore]
    public abstract bool IsConfigured { get; }

    public abstract Task<Expression> EvaluateAsync(
        IRuleExecutor ruleExecutor, 
        GetFieldValueDelegate getFieldValueCallback
    );
    public abstract string ToJson(JsonSerializerOptions options);
}
