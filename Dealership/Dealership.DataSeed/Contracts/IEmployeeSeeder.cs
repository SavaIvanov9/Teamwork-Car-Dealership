using Dealership.DataSeed.Models;
using System.Collections.Generic;

namespace Dealership.DataSeed.Contracts
{
    public interface IEmployeeSeeder
    {
        void SeedEmployees(IEnumerable<EmployeeDto> employees);
    }
}
