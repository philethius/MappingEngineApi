using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models.Operators;
using System.Linq.Expressions;
using Tests.Rules.Mocks;

namespace Tests.Rules.Tests.Models.Operators;

[TestClass]
public class AlwaysOperatorTests
{
    private readonly FakeRulesManager _rulesManager = new FakeRulesManager();

    [TestMethod]
    public void ItIsConfiguredByDefault()
    {
        // Arrange/Act
        var oper = new AlwaysOperator();

        // Assert
        Assert.IsTrue(oper.IsConfigured);
    }

    [TestMethod]
    public async Task ItAlwaysReturnsTrue()
    {
        // Arrange
        var oper = new AlwaysOperator();

        // Act
        var output = Expression.Lambda<Func<bool>>(
            await oper.EvaluateAsync(_rulesManager, _rulesManager.GetFieldValueCallback)
        )
        .Compile()();

        // Assert
        Assert.IsTrue(output);
    }
}
