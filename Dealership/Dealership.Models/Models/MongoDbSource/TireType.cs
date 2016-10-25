using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class TireType : ITireTyre
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public TireType()
        {

        }

        public TireType(string name)
        {
            this.Type = name;
        }

        public int Id { get; set; }

        public string Type { get; set; }
    }
}
