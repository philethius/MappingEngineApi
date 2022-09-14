using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Mappings;
using System.Linq.Expressions;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Operators;

public abstract class LogicOperator : Operator
{
    public Operator LeftOperand { get; set; }
    public Operator RightOperand { get; set; }

    public override bool IsConfigured => LeftOperand != null && RightOperand != null;
}

public class AndLogicOperator: LogicOperator
{
    public override string Type => "And";

    public override async Task<Expression> EvaluateAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        return Expression.And(
            await LeftOperand.EvaluateAsync(ruleExecutor, getFieldValueCallback),
            await RightOperand.EvaluateAsync(ruleExecutor, getFieldValueCallback)
        );
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}

public class OrLogicOperator : LogicOperator
{
    public override string Type => "And";

    public override async Task<Expression> EvaluateAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        return Expression.Or(
            await LeftOperand.EvaluateAsync(ruleExecutor, getFieldValueCallback),
            await RightOperand.EvaluateAsync(ruleExecutor, getFieldValueCallback)
        );
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}