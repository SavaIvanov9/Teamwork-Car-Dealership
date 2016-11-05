using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata.Fluent;

namespace Dealership.MySQL
{
    public partial class FluentModelMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var jsonReportMapping = new MappingConfiguration<JsonReport>();
            jsonReportMapping.MapType(jsonReport => new
            {
                Id = jsonReport.Id,
                JsonConent = jsonReport.JsonContent
            }).ToTable("JsonReport");
            jsonReportMapping.HasProperty(j => j.Id).IsIdentity();

            configurations.Add(jsonReportMapping);

            return configurations;
        }
    }
}
