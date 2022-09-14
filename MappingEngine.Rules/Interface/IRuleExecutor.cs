using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingEngine.Rules.Interface
{
    public interface IRuleExecutor
    {
        Task<RuleExecutionResult[]> ExecuteRuleAsync(
            string jsonRule,
            GetFieldValueDelegate getFieldValueCallback);

        Task<RuleExecutionResult[]> ExecuteRuleAsync(
            Rule rule,
            GetFieldValueDelegate getFieldValueCallback);
    }
}
