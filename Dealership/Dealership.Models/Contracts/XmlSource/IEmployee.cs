using System;

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
    }
}
