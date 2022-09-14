﻿using MappingEngine.Rules.Models.Mappings;

namespace Tests.Rules.Tests.Models.Mappings;

[TestClass]
public class AdditionMathematicalValueMappingTests
{
    [TestMethod]
    public async Task ItAddsValues()
    {
        // Arrange
        var leftValue = new ConstantValueMapping
        {
            MappingField = "Left",
            Value = 2
        };

        var rightValue = new ConstantValueMapping
        {
            MappingField = "Right",
            Value = 3
        };
        var mapping = new AdditionMathematicalValueMapping
        {
            MappingField = "Target",
            LeftMapping = leftValue,
            RightMapping = rightValue
        };

        // Act
        var output = await mapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.AreEqual(5, output);
    }

    private async Task<object> GetFieldValue(string field)
    {
        return await Task.Run(() => (object)null);
    }
}
