using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class TireType : ITireTyre
    {
        private ICollection<Tire> tires;

        [Obsolete("Only needed for serialization and materialization", true)]
        public TireType()
        {
            this.tires = new HashSet<Tire>();
        }

        public TireType(string name)
        {
            this.Type = name;
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Tire> Tires
        {
            get
            {
                return this.tires;
            }

            set
            {
                this.tires = value;
            }
        }
    }
}
