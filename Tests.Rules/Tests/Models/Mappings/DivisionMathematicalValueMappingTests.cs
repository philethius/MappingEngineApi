using MappingEngine.Rules.Enums;
using MappingEngine.Rules.Models.Mappings;

namespace Tests.Rules.Tests.Models.Mappings;

[TestClass]
public class DivisionMathematicalValueMappingTests
{
    [TestMethod]
    public async Task ItDividesValuesEvenly()
    {
        // Arrange
        var leftValue = new ConstantValueMapping
        {
            Value = 6
        };
        var rightValue = new ConstantValueMapping
        {
            Value = 3
        };
        var mapping = new DivisionMathematicalValueMapping
        {
            LeftMapping = leftValue,
            RightMapping = rightValue
        };

        // Act
        var output = await mapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.AreEqual(2, output);
    }

    [TestMethod]
    public async Task ItDivdesValuesWithoutDecimal()
    {
        // Arrange
        var leftValue = new ConstantValueMapping
        {
            Value = 5
        };
        var rightValue = new ConstantValueMapping
        {
            Value = 3
        };
        var mapping = new DivisionMathematicalValueMapping
        {
            LeftMapping = leftValue,
            RightMapping = rightValue
        };

        // Act
        var output = await mapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.AreEqual(1, output);
    }

    [TestMethod]
    public async Task ItDividesValuesWithDecimal()
    {
        // Arrange
        var leftValue = new ConstantValueMapping
        {
            Value = 4.5m
        };
        var rightValue = new ConstantValueMapping
        {
            Value = 3m
        };
        var mapping = new DivisionMathematicalValueMapping
        {
            LeftMapping = leftValue,
            RightMapping = rightValue
        };

        // Act
        var output = await mapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.AreEqual(1.5m, output);
    }

    private async Task<object> GetFieldValue(string field)
    {
        return await Task.Run(() => (object)null);
    }
}
