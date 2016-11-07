using System;
namespace Dealership.ExcelReportGenerator.Contracts
{
    public interface IExcelReportGenerator
    {
        void GenerateExcelReport(string pathToSave, string excelReportName);
    }
}
