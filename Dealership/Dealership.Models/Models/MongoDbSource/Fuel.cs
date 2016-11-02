using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Fuel : IFuel
    {
        private ICollection<Vehicle> vehicles;

        public Fuel()
        {
            this.vehicles = new HashSet<Vehicle>();
        }

        public Fuel(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }

            set
            {
                this.vehicles = value;
            }
        }
    }
}
