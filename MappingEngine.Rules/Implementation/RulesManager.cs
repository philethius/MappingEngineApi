using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models;
using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Serialization;
using System.Linq.Expressions;

namespace MappingEngine.Rules.Implementation;

public class RulesManager : IRulesManager
{
    public async Task<RuleExecutionResult[]> ExecuteRuleAsync(
        string jsonRule, 
        GetFieldValueDelegate getFieldValueCallback)
    {
        var rule = SerializationHelper.GetRuleFromJson(jsonRule);
        return await ExecuteRuleAsync(rule, getFieldValueCallback);
    }

    public async Task<RuleExecutionResult[]> ExecuteRuleAsync(
        Rule rule,
        GetFieldValueDelegate getFieldValueCallback)
    {
        var result =
            Expression.Lambda<Func<bool>>(
                await rule.Operator.EvaluateAsync(this, getFieldValueCallback))
            .Compile()();

        var results = new List<RuleExecutionResult>();
        if (result)
        {
            if (!rule.IfTrueMappings.Any()) { return null; }
            foreach (var mapping in rule.IfTrueMappings)
            {
                results.Add(new RuleExecutionResult
                {
                    Target = mapping.MappingField,
                    TargetValue = await mapping.GetOutputAsync(this, getFieldValueCallback)
                });
            }
        }
        else
        {
            if (!rule.IfFalseMappings.Any()) { return null; }
            foreach (var mapping in rule.IfFalseMappings)
            {
                results.Add(new RuleExecutionResult
                {
                    Target = mapping.MappingField,
                    TargetValue = await mapping.GetOutputAsync(this, getFieldValueCallback)
                });
            }
        }

        return results.ToArray();
    }
}
