using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class BatteryBrand : IBatteryBrand
    {
        private ICollection<Battery> batteries;

        [Obsolete("Only needed for serialization and materialization", true)]
        public BatteryBrand()
        {
            this.batteries = new HashSet<Battery>();
        }

        public BatteryBrand(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Battery> Batteries
        {
            get
            {
                return this.batteries;
            }

            set
            {
                this.batteries = value;
            }
        }
    }
}
