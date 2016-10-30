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

        public SalesReportsReaderExcel(string connectonString)
        {
            this.ConnectionString = connectonString;
        }
        public ExcelSalesReport ReadReport(string reportPath, string reportDate)
        {
            ExcelSalesReport report = new ExcelSalesReport();
            report.DateOfSale = DateTime.Parse(reportDate);
            /* string LocalconnectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                 @"Data Source= {0};"+
                                                 @"Extended Properties='Excel 12.0 Xml;HDR=YES;'"
  , reportPath);*/
            string LocalconnectionString = string.Format(this.ConnectionString, reportPath);
            using (OleDbConnection connection = new OleDbConnection(LocalconnectionString)) //TODO: fix coupling, maybe use factory
            {
                connection.Open();
                OleDbCommand excelCommand = new OleDbCommand("SELECT * FROM [Sales$]", connection);

                using (var oleDbDataAdapter = new OleDbDataAdapter(excelCommand))
                {
                    var dataSet = new DataSet();
                    oleDbDataAdapter.Fill(dataSet);

                    using (var reader = dataSet.CreateDataReader())
                    {
                        report.DistributorName = this.ReadStoreName(reader); //TODO PARSE IT
                        //report.Location = 
                        reader.Read(); // Skip column names
                        report.Records = this.GetReportEntries(reader);
                    }
                }
                /*
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["Name"];
                        var score = reader["Score"];
                        Console.WriteLine("Name - {0}; Score - {1}", name, score);

                    }
                }*/
            }

            return report;
        }

        private string ReadStoreName(DataTableReader reader)
        {
            if (reader.Read())
            {
                return reader[0].ToString();
            }

            return null;
        }

        private ICollection<ExcelSalesReportEntry> GetReportEntries(DataTableReader reader)
        {
            var reportEntries = new List<ExcelSalesReportEntry>();

            while (reader.Read())
            {
                var isLastRow = string.IsNullOrEmpty(reader[1].ToString());

                if (!isLastRow)
                {
                    reportEntries.Add(new ExcelSalesReportEntry()
                    {
                        ProductId = int.Parse(reader[0].ToString()),
                        Quantity = int.Parse(reader[1].ToString()),
                        UnitPrice = decimal.Parse(reader[2].ToString()),
                    });
                }
            }

            return reportEntries;
        }
    }
}
