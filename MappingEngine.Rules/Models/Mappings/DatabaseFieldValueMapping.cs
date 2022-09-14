using MappingEngine.Rules.Interface;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MappingEngine.Rules.Models.Mappings;

public class DatabaseFieldValueMapping : Mapping
{
    public override string Type => "From database field";

    public string SourceField { get; set; }

    //[JsonIgnore]
    //public GetFieldValueDelegate Callback { get; set; }

    protected override bool IsDerivedConfigured => SourceField != null;// && Callback != null;

    public override async Task<object> GetOutputAsync(
        IRuleExecutor ruleExecutor,
        GetFieldValueDelegate getFieldValueCallback)
    {
        if (!IsConfigured)
        {
            throw new Exception($"Cannot retrieve database field '{SourceField}', {nameof(DatabaseFieldValueMapping)} is not properly configured.");
        }

        return await getFieldValueCallback(SourceField);

        //return await Callback(Field);
    }

    public override string ToJson(JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(this, options);
    }
}
