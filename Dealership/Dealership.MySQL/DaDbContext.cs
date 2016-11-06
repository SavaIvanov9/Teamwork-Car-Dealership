using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace Dealership.MySQL
{
    public partial class DaDbContext : OpenAccessContext
    {
        private static string connectionStringName = @"DealershipReports";
        private static BackendConfiguration backend = GetBackendConfiguration();
        private static MetadataSource metadataSource = new FluentModelMetadataSource();

        public DaDbContext()
                : base(connectionStringName, backend, metadataSource)
            { }

        public IQueryable<JsonReport> JsonReports
        {
            get
            {
                return this.GetAll<JsonReport>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MySql";
            backend.ProviderName = "MySql.Data.MySqlClient";

            return backend;
        }
    }
}
