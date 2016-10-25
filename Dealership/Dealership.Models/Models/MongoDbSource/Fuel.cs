using System;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Fuel
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public Fuel()
        {

        }

        public Fuel(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
