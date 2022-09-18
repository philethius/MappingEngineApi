using MappingEngine.Rules.Models.Mappings;

namespace Tests.Rules.Tests.Models.Mappings;

[TestClass]
public class ConstantModelMappingTests
{
    [TestMethod]
    public void ItIsConfigured()
    {
        // Arrange/Act
        var constantMapping = new ConstantValueMapping
        {
            MappingField = "test"
        };

        // Assert
        Assert.IsTrue(constantMapping.IsConfigured);
    }

    [TestMethod]
    public async Task ItReturnsNullByDefault()
    {
        // Arrange
        var constantMapping = new ConstantValueMapping();

        // Act
        var value = await constantMapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.IsNull(value);
    }

    [TestMethod]
    public async Task ItReturnsExpectedValue()
    {
        // Arrange
        var expectedValue = 23;
        var constantMapping = new ConstantValueMapping
        {
            Value = expectedValue
        };

        // Act
        var value = await constantMapping.GetOutputAsync(null, GetFieldValue);

        // Assert
        Assert.AreEqual(expectedValue, value);
    }

    private async Task<object> GetFieldValue(string field)
    {
        return await Task.Run(() => (object)null);
    }
}
