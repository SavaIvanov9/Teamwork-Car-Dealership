using Dealership.Models.Contracts.XmlSource;
using System;

namespace Dealership.DataSeed.Models
{
    public class EmployeeDto : IEmployee
    {
        public IAddress Address { get; set; }

        public string FirstName { get; set; }

        public DateTime HireDate { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Pid { get; set; }

        public IPosition Position { get; set; }

        public int Salary { get; set; }

        public IShop Shop { get; set; }
    }
}
