using Dealership.Reports.Models;
using DealerShip.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace Dealership.ExcelFilesProcessing
{
    public class SalesReportsReaderExcel
    {
        private readonly string ConnectionString;
        private int LeftOffset = 0;

        public SalesReportsReaderExcel(string connectonString)
        {
            this.ConnectionString = connectonString;
        }
        private void ReadSheet(ExcelSalesReport report, string Name, OleDbConnection connection)
        {
            OleDbCommand excelCommand = new OleDbCommand("SELECT * FROM [" + Name + " Sales$]", connection);

            using (var oleDbDataAdapter = new OleDbDataAdapter(excelCommand))
            {
                var dataSet = new DataSet();
                oleDbDataAdapter.Fill(dataSet);

                using (var reader = dataSet.CreateDataReader())
                {
                    reader.Read();
                    if (string.IsNullOrEmpty(reader[0].ToString()))
                    {
                        LeftOffset = 1;
                    }
                    else
                    {
                        LeftOffset = 0;
                    }

                    report.DistributorName = reader[LeftOffset + 0].ToString();
                    //report.Location = 
                    reader.Read(); // Skip column names
                    this.GetReportEntries(reader, Name, report.Records);
                }
            }

        }

        public ExcelSalesReport ReadReport(string reportPath, string reportDate)
        {
            //reportPath=D:\Julii\last\Teamwork-Car-Dealership\Dealership\Data\Sample-Sales-Reports\20-Jul-2016\Calgary-South_Pro_Automotive-Sales-Report-20-Jul-2016.xls
            ExcelSalesReport report = new ExcelSalesReport();
            report.DateOfSale = DateTime.Parse(reportDate);
            string LocalconnectionString = string.Format(this.ConnectionString, reportPath);
            using (OleDbConnection connection = new OleDbConnection(LocalconnectionString)) //TODO: fix coupling, maybe use factory
            {
                connection.Open();
                ReadSheet(report, "Vehicle", connection);
            }

            return report;
        }

        private void GetReportEntries(DataTableReader reader, string TypeName, ICollection<ExcelSalesReportEntry> reportEntries)
        {
            while (reader.Read())
            {
                var isLastRow = string.IsNullOrEmpty(reader[LeftOffset + 1].ToString());

                if (!isLastRow)
                {
                    ExcelSalesReportEntry entity = new ExcelSalesReportEntry();
                    entity.VehicleModel = reader[LeftOffset + 0].ToString();
                    entity.EmployeeId = int.Parse(reader[LeftOffset + 1].ToString());
                    entity.Quantity = int.Parse(reader[LeftOffset + 2].ToString());
                    entity.UnitPrice = decimal.Parse(reader[LeftOffset + 3].ToString());
                    reportEntries.Add(entity);
                }
            }
        }
    }
}
