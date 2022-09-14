using MappingEngine.Rules.Models;

namespace MappingEngine.Services.Interface;

public interface IRulesService
{
    Rule Rule { get; set; }  // TODO: REMOVE - temporary testing accessor
    Task<T> MapAsync<T>() where T : new();
}
