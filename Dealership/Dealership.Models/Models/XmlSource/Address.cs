using Dealership.Models.Contracts;
using Dealership.Models.Contracts.XmlSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class Address : IAddress, IEntity
    {
        private string street;
        private string zipCode;

        public Address()
        {
            this.Employees = new List<Employee>();
            this.Shops = new List<Shop>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [Required]
        [StringLength(50)]
        public string Street
        {
            get
            {
                return this.street;
            }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Streer can not be null!");
                }
                this.street = value;
            }
        }

        [Required]
        [StringLength(20)]
        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Zip Code can not be null!");
                }
                this.zipCode = value;
            }
        }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
     }
}
