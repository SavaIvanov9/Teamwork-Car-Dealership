using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class TireBrand : ITireBrand
    {
        private ICollection<Tire> tires;

        [Obsolete("Only needed for serialization and materialization", true)]
        public TireBrand()
        {
            this.tires = new HashSet<Tire>();
        }

        public TireBrand(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

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
