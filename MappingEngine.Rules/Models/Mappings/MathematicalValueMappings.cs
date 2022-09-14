using MappingEngine.Rules.Interface;
using System.Linq.Expressions;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

public abstract class MathematicalValueMapping : Mapping
{
    public Mapping LeftMapping { get; set; }
    public Mapping RightMapping { get; set; }

    public override async Task<object> GetOutputAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback
    )
    {
        if (!IsConfigured)
        {
            throw new Exception($"Cannot derive value of mathematical operation '{Type}', {GetType().Name} is not configured.");
        }

        var leftObj = await LeftMapping.GetOutputAsync(ruleExecutor, getFieldValueCallback);
        var rightObj = await RightMapping.GetOutputAsync(ruleExecutor, getFieldValueCallback);
        var isLeftLong = long.TryParse(leftObj.ToString(), out long leftLong);
        var isRightLong = long.TryParse(rightObj.ToString(), out long rightLong);
        var isDecimal = !isLeftLong || !isRightLong;

        Expression expression;
        if (isDecimal)
        {
            var leftDec = leftObj is decimal ? (decimal)leftObj : decimal.Parse((string)leftObj);
            var rightDec = rightObj is decimal ? (decimal)rightObj : decimal.Parse((string)rightObj);
            expression = GetExpressionFrom(leftDec, rightDec);
        }
        else
        {
            expression = GetExpressionFrom(leftLong, rightLong);
        }

        if (isDecimal) { return Expression.Lambda<Func<decimal>>(expression).Compile()(); }

        // try to parse as the small size first
        var output = Expression.Lambda<Func<long>>(expression).Compile()();
        if (int.TryParse(output.ToString(), out int intOutput)) { return intOutput; }

        // return type = long
        return output;
    }

    protected abstract Expression GetExpressionFrom<T>(T left, T right);
}

public class AdditionMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From sum of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(T left, T right)
    {
        return Expression.Add(Expression.Constant(left), Expression.Constant(right));
    }
}

public class SubtractionMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From difference of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(T left, T right)
    {
        return Expression.Subtract(Expression.Constant(left), Expression.Constant(right));
    }
}

public class MultiplicationMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From product of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(T left, T right)
    {
        return Expression.Multiply(Expression.Constant(left), Expression.Constant(right));
    }
}

public class DivisionMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From quotient of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(T left, T right)
    {
        return Expression.Divide(Expression.Constant(left), Expression.Constant(right));
    }
}

public class ModulusMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From equivalence of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(T left, T right)
    {
        return Expression.Modulo(Expression.Constant(left), Expression.Constant(right));
    }
}