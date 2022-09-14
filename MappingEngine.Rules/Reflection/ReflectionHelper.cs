using MappingEngine.Rules.Interface;
using System.Reflection;

namespace MappingEngine.Rules.Reflection;

public static class ReflectionHelper
{
    public static IDictionary<string, Type> GetAvailableOf<T>() where T : ITypeName
    {
        var dictionary = new Dictionary<string, Type>();

        // find all public concrete class types of type Mapping
        var types = Assembly.GetExecutingAssembly().GetExportedTypes()
            .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);

        foreach (var type in types)
        {
            var inst = (T)Activator.CreateInstance(type);
            dictionary.Add(inst.Type, type);            
        }

        return dictionary;
    }
}
