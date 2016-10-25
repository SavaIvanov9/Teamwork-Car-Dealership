using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Brand : IBrand
    {

        [Obsolete("Only needed for serialization and materialization", true)]
        public Brand()
        {
            
        }

        public Brand(string name)
        {
            this.Name = name;
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
