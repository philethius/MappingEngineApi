using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models;
using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Services.Interface;

namespace MappingEngine.Services.Implementation;

public class RulesService : IRulesService
{
    private readonly IRulesManager _rulesManager;

    public Rule Rule { get; set; }  // TODO: REMOVE - temporary testing accessor

    public RulesService(IRulesManager rulesManager)
    {
        _rulesManager = rulesManager;
    }

    public async Task<T> MapAsync<T>() where T : new()
    {
        var instance = new T();

        var output = await _rulesManager.ExecuteRuleAsync(Rule, SendSingleValue(123));

        return instance;
    }

    #region - TODO: REMOVE - temporary
    private GetFieldValueDelegate SendSingleValue(object value)
    {
        return (string field) => GetSingleFieldValue(field, value);
    }

    private async Task<object> GetSingleFieldValue(string field, object value)
    {
        return await Task.Run(() => value);
    }
    #endregion
}
