using Dealership.Models.Contracts;
using Dealership.Models.Contracts.XmlSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class Country : ICountry, IEntity
    {
        private string name;

        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name can not be null!");
                }
                this.name = value;
            }
        }

        public virtual ICollection<City> Cities { get; set; }
    }
}