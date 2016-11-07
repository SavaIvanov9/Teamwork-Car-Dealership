using System;
using Dealership.Models.Contracts.XmlSource;
using System.ComponentModel.DataAnnotations;
using Dealership.Models.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class Position : IPosition, IEntity
    {
        private string name;

        public Position()
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

        public virtual ICollection<Employee> Employees { get; set; }
    }
}