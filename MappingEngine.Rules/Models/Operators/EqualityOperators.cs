using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Mappings;
using System.Linq.Expressions;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Operators;

public abstract class EqualityOperator : Operator
{
    public override bool IsConfigured => Target != null && Value != null;

    public Mapping Target { get; set; }
    public Mapping Value { get; set; }

    public override async Task<Expression> EvaluateAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        object target = await Target.GetOutputAsync(
            ruleExecutor,
            getFieldValueCallback);
        object value = await Value.GetOutputAsync(
            ruleExecutor,
            getFieldValueCallback);

        if (target is string && ((string)target).Equals("true", StringComparison.OrdinalIgnoreCase)) { target = true; }
        if (target is string && ((string)target).Equals("false", StringComparison.OrdinalIgnoreCase)) { target = false; }

        if (value is string && ((string)value).Equals("true", StringComparison.OrdinalIgnoreCase)) { value = true; }
        if (value is string && ((string)value).Equals("false", StringComparison.OrdinalIgnoreCase)) { value = false; }

        return GetEqualityExpression(target, value);
    }

    protected abstract BinaryExpression GetEqualityExpression(object target, object value);
}

public class EqualsEqualityOperator : EqualityOperator
{
    public override string Type => "Equals";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.Equal(Expression.Constant(target), Expression.Constant(value));
    }
}

public class NotEqualsEqualityOperator : EqualityOperator
{
    public override string Type => "Not equals";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.NotEqual(Expression.Constant(target), Expression.Constant(value));
    }
}

public class GreaterThanEqualityOperator : EqualityOperator
{
    public override string Type => "Greater than";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.GreaterThan(Expression.Constant(target), Expression.Constant(value));
    }
}

public class GreaterThanOrEqualsEqualityOperator : EqualityOperator
{
    public override string Type => "Greater than or equals";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.GreaterThanOrEqual(Expression.Constant(target), Expression.Constant(value));
    }
}

public class LessThanEqualityOperator : EqualityOperator
{
    public override string Type => "Less than";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.LessThan(Expression.Constant(target), Expression.Constant(value));
    }
}

public class LessThanOrEqualsEqualityOperator : EqualityOperator
{
    public override string Type => "Less than or equals";

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override BinaryExpression GetEqualityExpression(object target, object value)
    {
        return Expression.LessThanOrEqual(Expression.Constant(target), Expression.Constant(value));
    }
}

