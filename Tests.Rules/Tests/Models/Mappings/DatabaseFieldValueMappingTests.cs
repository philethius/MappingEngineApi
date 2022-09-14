using MappingEngine.Rules.Models.Mappings;
using Newtonsoft.Json.Linq;

namespace Tests.Rules.Tests.Models.Mappings;

[TestClass]
public class DatabaseFieldValueMappingTests
{
    [TestMethod]
    public void ItIsNotConfiguredByDefault()
    {
        // Arrange/Act
        var mapping = new DatabaseFieldValueMapping();

        // Assert
        Assert.IsFalse(mapping.IsConfigured);
    }

    [TestMethod]
    public void ItCanConfigure()
    {
        // Arrange/Act
        var mapping = new DatabaseFieldValueMapping
        {
            MappingField = "Target",
            SourceField = "Source"
        };

        // Assert
        Assert.IsTrue(mapping.IsConfigured);
    }

    [TestMethod]
    public async Task ItReturnsAValue()
    {
        // Arrange
        var value = "34";
        var mapping = new DatabaseFieldValueMapping
        {
            MappingField = "Target",
            SourceField = "Source"
        };

        // Act
        var output = await mapping.GetOutputAsync(null, SendSingleValue(value));

        // Assert
        Assert.AreEqual(value, output);
    }

    [TestMethod]
    public async Task ItReturnsCorrectValueFromField()
    {
        // Arrange
        var fields = SetUpFieldTestingModel();
        var mapping = new DatabaseFieldValueMapping
        {
            MappingField = "Target",
            SourceField = FieldTestingModel.Field2Name
        };

        // Act
        var output = await mapping.GetOutputAsync(null, SendModelValue(fields));

        // Assert
        Assert.AreEqual(fields.Field2, output);
    }

    #region Single Value Return
    private GetFieldValueDelegate SendSingleValue(string value)
    {
        return (string field) => GetSingleFieldValue(field, value);
    }

    private async Task<object> GetSingleFieldValue(string field, string value)
    {
        return await Task.Run(() => value);
    }
    #endregion

    #region Test Model Lookup
    private GetFieldValueDelegate SendModelValue(FieldTestingModel model)
    {
        return (string field) => GetValueFromFieldTestingModel(field, model);
    }

    private async Task<object> GetValueFromFieldTestingModel(string field, FieldTestingModel model)
    {
        object val;
        switch (field)
        {
            case FieldTestingModel.Field1Name:
                val = model.Field1;
                break;
            case FieldTestingModel.Field2Name:
                val = model.Field2;
                break;
            case FieldTestingModel.Field3Name:
                val = model.Field3;
                break;
            default: throw new Exception($"Unexpected field name '{field}'");
        }

        return await Task.Run(() => val);
    }

    private static FieldTestingModel SetUpFieldTestingModel()
    {
        return new FieldTestingModel
        {
            Field1 = "111",
            Field2 = "222",
            Field3 = "333"
        };
    }
    #endregion
}
