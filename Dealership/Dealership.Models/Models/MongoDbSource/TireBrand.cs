using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class TireBrand : ITireBrand
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public TireBrand()
        {

        }

        public TireBrand(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
