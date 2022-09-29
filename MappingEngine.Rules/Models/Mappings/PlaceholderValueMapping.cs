using MappingEngine.Rules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MappingEngine.Rules.Models.Mappings
{
    public class PlaceholderValueMapping : Mapping
    {
        public override string Type => "From placeholder";

        protected override bool IsDerivedConfigured => true;

        public override Task<object> GetOutputAsync(IRuleExecutor ruleExecutor, GetFieldValueDelegate getFieldValueCallback)
        {
            throw new NotImplementedException();
        }

        public override string ToJson(JsonSerializerOptions options)
        {
            return JsonSerializer.Serialize(this, options);
        }
    }
}
