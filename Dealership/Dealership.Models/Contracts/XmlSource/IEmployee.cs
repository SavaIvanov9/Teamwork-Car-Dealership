using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models.Contracts.XmlSource
{
    public interface IEmployee
    {
        string FirstName { get; }

        string LastName { get; }

        string Pid { get; }

        string Phone { get; }

        DateTime HireDate { get; }

        int Salary { get; }

        IPosition Position { get; }

        IShop Shop { get; }

        IAddress Address { get; }
    }
}
