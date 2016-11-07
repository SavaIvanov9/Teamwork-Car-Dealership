using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Brand : IBrand
    {
        private ICollection<Vehicle> vehicles;

        public Brand()
        {
            this.vehicles = new HashSet<Vehicle>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
