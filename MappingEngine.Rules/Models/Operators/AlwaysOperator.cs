using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MappingEngine.Rules.Models.Operators
{
    public class AlwaysOperator : Operator
    {
        public override string Type => "Always";

        public override bool IsConfigured => true;

        public override async Task<Expression> EvaluateAsync(
            IRuleExecutor ruleExecutor,
            GetFieldValueDelegate getFieldValueCallback
        )
        {
            return await Task.Run(() => Expression.Equal(Expression.Constant(true), Expression.Constant(true)));
        }

        public override string ToJson(JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(this, options);
        }
    }
}
