using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Reflection;

namespace Tests.Rules.Tests.Reflection
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void ItReturnAvailableMappings()
        {
            // Act
            var mappings = ReflectionHelper.GetAvailableOf<Mapping>();

            // Assert
            Assert.AreNotEqual(0, mappings.Count);
        }
    }
}