using MappingEngine.Rules.Implementation;
using MappingEngine.Rules.Interface;
using MappingEngine.Rules.Models;
using MappingEngine.Rules.Models.Mappings;
using MappingEngine.Rules.Models.Operators;
using MappingEngine.Services.Implementation;
using MappingEngine.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder().Build();

var rulesService = host.Services.GetService<IRulesService>();
rulesService.Rule = new Rule
{
    IfTrueMappings = new Mapping[] { 
            new ConstantValueMapping
            {
                MappingField = "TargetField",
                Value = "333"
            }
        },
    Operator = new AlwaysOperator()
};
await rulesService.MapAsync<object>();

// run until completion
await host.StartAsync();

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            //app.AddJsonFile("");
            //app.AddJsonFile("", true);
        })
        .ConfigureServices((hostingContext, services) =>
        {
            services.AddScoped<IRulesManager, RulesManager>();
            services.AddScoped<IRulesService, RulesService>();
        });

