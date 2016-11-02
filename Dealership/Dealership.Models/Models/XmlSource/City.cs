using Dealership.Models.Contracts;
using Dealership.Models.Contracts.XmlSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class City : ICity, IEntity
    {
        private string name;

        public City()
        {
            this.Addresses = new HashSet<Address>();
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

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}