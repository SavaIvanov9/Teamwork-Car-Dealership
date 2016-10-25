using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Brand : IBrand
    {
        private ICollection<Vehicle> vehicles;

        [Obsolete("Only needed for serialization and materialization", true)]
        public Brand()
        {
            this.vehicles = new HashSet<Vehicle>();
        }

        public Brand(string name)
        {
            this.Name = name;           
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
