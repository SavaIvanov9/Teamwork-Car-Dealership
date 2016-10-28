using Dealership.DataSeed.Models;
using System.Collections.Generic;

namespace Dealership.DataSeed.Contracts
{
    public interface IEmployeeReader
    {
        IEnumerable<EmployeeDto> ReadEmployees();
    }
}
