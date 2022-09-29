using MappingEngine.Rules.Enums;
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
        var isDecimal = LeftMapping.DataType == typeof(decimal) 
                || RightMapping.DataType == typeof(decimal);
        var isInt = LeftMapping.DataType == typeof(int)
            && RightMapping.DataType == typeof(int);

        Expression expression;
        if (isDecimal)
        {
            expression = GetExpressionFrom<decimal>(leftObj, rightObj);
            DataType = typeof(decimal);
            return Expression.Lambda<Func<decimal>>(expression).Compile()();
        }
        else if (isInt)
        {
            expression = GetExpressionFrom<long>(leftObj, rightObj);
            var output = Expression.Lambda<Func<long>>(expression).Compile()();

            // up-converting to long (no int overflow)
            if (output > int.MaxValue) 
            {
                DataType = typeof(long);
                return output;
            }

            DataType = typeof(int);
            return Convert.ToInt32(output);
        }
        else
        {
            // long by definition
            expression = GetExpressionFrom<long>(leftObj, rightObj);
            DataType = typeof(long);
            return Expression.Lambda<Func<long>>(expression).Compile()();
        }
    }

    protected abstract Expression GetExpressionFrom<T>(object left, object right);
}

public class AdditionMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From sum of";

    protected override bool IsDerivedConfigured => LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(object left, object right)
    {
        return Expression.Add(Expression.Constant((T)left), Expression.Constant((T)right));
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

    protected override Expression GetExpressionFrom<T>(object left, object right)
    {
        return Expression.Subtract(Expression.Constant((T)left), Expression.Constant((T)right));
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

    protected override Expression GetExpressionFrom<T>(object left, object right)
    {
        return Expression.Multiply(Expression.Constant((T)left), Expression.Constant((T)right));
    }
}

public class DivisionMathematicalValueMapping : MathematicalValueMapping
{
    public override string Type => "From quotient of";

    //public DivisionMode DivisionMode { get; set; } = DivisionMode.WithoutDecimal;

    protected override bool IsDerivedConfigured => 
        LeftMapping != null && RightMapping != null;

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }

    protected override Expression GetExpressionFrom<T>(object left, object right)
    {
        //if (DivisionMode == DivisionMode.WithDecimal)
        //{
        //    return Expression.Divide(
        //        Expression.Constant(Convert.ToDecimal(left)), 
        //        Expression.Constant(Convert.ToDecimal(right))
        //    );
        //}

        return Expression.Divide(Expression.Constant((T)left), Expression.Constant((T)right));
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

    protected override Expression GetExpressionFrom<T>(object left, object right)
    {
        return Expression.Modulo(Expression.Constant((T)left), Expression.Constant((T)right)); ;
    }
}