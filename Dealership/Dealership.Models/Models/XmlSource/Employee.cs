using System;
using Dealership.Models.Contracts.XmlSource;
using Dealership.Models.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dealership.Models.Models.XmlSource
{
    public class Employee : IEmployee, IEntity
    {
        private string firsName;
        private string lastName;
        private string pid;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AddressId { get; set; }

        public Address Address { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName
        {
            get
            {
                return this.firsName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Firs Name can not be null!");
                }
                this.firsName = value;
            }
        }

        public DateTime HireDate { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Last Name can not be null!");
                }
                this.lastName = value;
            }
        }

        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Pid
        {
            get
            {
                return this.pid;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Pid can not be null!");
                }
                this.pid = value;
            }
        }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public int Salary { get; set; }

        public int ShopId { get; set; }

        public Shop Shop { get; set; }
    }
}