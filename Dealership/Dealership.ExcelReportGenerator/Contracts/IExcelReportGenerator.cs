using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.ExcelReportGenerator.Contracts
{
    public interface IExcelReportGenerator
    {
        void GenerateExcelReport(string pathToSave, string excelReportName);
    }
}
