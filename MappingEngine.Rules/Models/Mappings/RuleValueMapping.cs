using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Serialization;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

public class RuleValueMapping : Mapping
{
    private string _jsonRule;
    private Rule _rule;

    public override string Type => "From rule";
    
    public string JsonRule
    {
        get => _jsonRule;
        set
        {
            _jsonRule = value;
            _rule = 
                JsonSerializer.Deserialize<Rule>(
                    _jsonRule, 
                    SerializationHelper.GetMappingEngineSerializerOptions()
                );
        }
    }

    public Rule Rule
    {
        get => _rule;
        set
        {
            _rule = value;
            _jsonRule =
                JsonSerializer.Serialize(
                    value,
                    typeof(Rule),
                    SerializationHelper.GetMappingEngineSerializerOptions()
                );
        }
    }

    protected override bool IsDerivedConfigured =>
        !string.IsNullOrWhiteSpace(_jsonRule) && _rule != null && _rule.IsConfigured;

    public override async Task<object> GetOutputAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback)
    {
        if (!IsConfigured)
        {
            throw new Exception($"Cannot retrieve value from rule, {nameof(RuleValueMapping)} is not configured.");
        }

        return await ruleExecutor.ExecuteRuleAsync(_rule, getFieldValueCallback);
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
