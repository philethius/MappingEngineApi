using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Mappings;
using System.Linq.Expressions;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Operators;

public abstract class UnaryOperator : Operator
{
    public Operator Operand { get; set; }

    public override bool IsConfigured => Operand != null;
}

public class NotUnaryOperator : UnaryOperator
{
    public override string Type => "Not";

    public override async Task<Expression> EvaluateAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        return Expression.Not(
            await Operand.EvaluateAsync(ruleExecutor, getFieldValueCallback)
        );
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
