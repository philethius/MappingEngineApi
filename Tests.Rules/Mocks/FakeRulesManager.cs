using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models;
using MappingEngine.Rules.Models.Mappings;

namespace Tests.Rules.Mocks;

public class FakeRulesManager : IRulesManager
{
    public GetFieldValueDelegate GetFieldValueCallback { get; set; }

    public async Task<RuleExecutionResult[]> ExecuteRuleAsync(string jsonRule, GetFieldValueDelegate getFieldValueCallback)
    {
        return await Task.Run(() => new RuleExecutionResult[0]);
    }

    public async Task<RuleExecutionResult[]> ExecuteRuleAsync(Rule rule, GetFieldValueDelegate getFieldValueCallback)
    {
        return await Task.Run(() => new RuleExecutionResult[0]);
    }
}
