using Dealership.Models.Contracts;
using Dealership.Models.Contracts.XmlSource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class Shop : IShop, IEntity
    {
        private string name;

        public Shop()
        {
            this.Employees = new HashSet<Employee>();
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

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}