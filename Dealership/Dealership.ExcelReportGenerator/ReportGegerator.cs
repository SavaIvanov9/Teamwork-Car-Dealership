using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Common;
using Dealership.ExcelReportGenerator.Contracts;
using Dealership.ExcelReportGenerator.Enums;
using Dealership.JsonReporter;
using Dealership.MySQL;
using Dealership.SQLite;
using Dealership.SQLite.Repositories;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Dealership.ExcelReportGenerator
{
    public class ReportGenerator : IExcelReportGenerator
    {
        private readonly DataAccessDbContext MySqlData;
        //private readonly ItemExpensesDbEntities SqliteData;
        private readonly IGenericRepository<Item> SQLiteData;

        private const int StartingColumn = 2;
        private const int EndingColumn = 9;

        private int bodyRowPosition = 3;

        public ReportGenerator()
        {
            this.MySqlData = new DataAccessDbContext();
            //this.SqliteData = new ItemExpensesDbEntities();

            this.SQLiteData = new GenericRepository<Item>(new ItemExpensesDbEntities());
        }

        public void GenerateExcelReport(string pathToSave, string excelReportName)
        {
            Console.WriteLine("Generating Excel report...");

            if (File.Exists(Path.Combine(pathToSave, excelReportName)))
            {
                Console.WriteLine("Excel report already exists.");
            }
            else
            {
                if (!string.IsNullOrEmpty(excelReportName))
                {
                    Utility.CreateDirectoryIfNotExists(pathToSave);
                }

                CreateReport(pathToSave, excelReportName);

                Console.WriteLine("Excel Report file created successfully.");
            }
        }

        private void CreateReport(string pathToSave, string excelReportName)
        {
            var fileInfo = new FileInfo(pathToSave + excelReportName);

            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Income Report");

                this.GenerateExcelHeadDocument(worksheet);
                this.GenerateExcelBodyDocument(worksheet);

                AutofitValues(worksheet);

                package.Save();
            }
        }

        private void GenerateExcelHeadDocument(ExcelWorksheet worksheet)
        {
            worksheet.Row(2).Height = 20;
            worksheet.Row(3).Height = 18;

            worksheet.Cells[2, 2].Value = "Income Report";
            var headerRange = worksheet.Cells[2, StartingColumn, 2, EndingColumn];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.Size = 16;
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;

            worksheet.Cells[3, (int)ReportColumns.ProductIdColumn].Value = "Product ID";
            worksheet.Cells[3, (int)ReportColumns.ProductNameColumn].Value = "Product Model";
            worksheet.Cells[3, (int)ReportColumns.ManufacturerNameColumn].Value = "Manufacturer";
            worksheet.Cells[3, (int)ReportColumns.QuantityColumn].Value = "Total Quantity Sold";
            worksheet.Cells[3, (int)ReportColumns.TotalIncomeColumn].Value = "Total Income";
            worksheet.Cells[3, (int)ReportColumns.ExpensePerItemColumn].Value = "Taxes Per Item";
            worksheet.Cells[3, (int)ReportColumns.TotalExpensesColumn].Value = "Total Expenses";
            worksheet.Cells[3, (int)ReportColumns.RevenueColumn].Value = "Income After Taxes";

            using (var range = worksheet.Cells[3, StartingColumn, 3, EndingColumn])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Black);
                range.Style.Font.Color.SetColor(Color.WhiteSmoke);
                range.Style.ShrinkToFit = false;
            }
        }

        private void GenerateExcelBodyDocument(ExcelWorksheet worksheet)
        {
            foreach (var jsonReport in this.MySqlData.JsonReports)
            {
                var currentEntity = JsonConvert.DeserializeObject<JsonReportEntry>(jsonReport.JsonContent);
                this.bodyRowPosition++;
                this.FillCurrentRowWithData(worksheet, currentEntity);
                this.StyleCurrentExcelRow(this.bodyRowPosition, worksheet);
            }
        }

        private void FillCurrentRowWithData(ExcelWorksheet worksheet, JsonReportEntry currentEntity)
        {
            var productId = currentEntity.ProductId;
            var productName = currentEntity.ProductName;
            var manufacturererName = currentEntity.ManufacturerName;
            var quantity = currentEntity.TotalQuantitySold;
            var totalIncome = currentEntity.TotalIncome;
            var expensePerItem = this.GetItemExpense(currentEntity);
            var totalExpenses = (totalIncome / 100) * decimal.Parse(expensePerItem.Substring(0, expensePerItem.Length - 1));
            var clearIncome = totalIncome - totalExpenses;

            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.ProductIdColumn].Value = string.Format("{0:}",
                productId);
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.ProductNameColumn].Value = productName;
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.ManufacturerNameColumn].Value = manufacturererName;
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.QuantityColumn].Value = string.Format("{0:}", quantity);
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.TotalIncomeColumn].Value = totalIncome.ToCurrency();
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.ExpensePerItemColumn].Value = expensePerItem;
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.TotalExpensesColumn].Value = totalExpenses.ToCurrency();
            worksheet.Cells[this.bodyRowPosition, (int)ReportColumns.RevenueColumn].Value = clearIncome.ToCurrency();
        }

        private string GetItemExpense(JsonReportEntry currentEntity)
        {
            //if (this.SqliteData
            //    .Items
            //    .Select(x => x.Name)
            //    .Contains(currentEntity.ProductName))
            //{
            //    var item = this.SqliteData
            //    .Items
            //    .Where(i => i.Name == currentEntity.ProductName)
            //    .Select(i => new
            //    {
            //        Expense = i.Taxes
            //    })
            //    .First();

            //    return item.Expense;
            //}

            //return "0%";

            if (this.SQLiteData
                .GetAll()
                .Select(x => x.Name)
                .Contains(currentEntity.ProductName))
            {
                var item = this.SQLiteData
                .GetAll(i => i.Name == currentEntity.ProductName)
                .Select(i => new
                {
                    Expense = i.Taxes
                })
                .First();

                return item.Expense;
            }

            return "0%";
        }

        private void StyleCurrentExcelRow(int currentRow, ExcelWorksheet worksheet)
        {
            using (var rowRange = worksheet.Cells[currentRow, StartingColumn, currentRow, EndingColumn])
            {
                rowRange.Style.Font.Bold = false;
                rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rowRange.Style.Fill.BackgroundColor.SetColor(Color.Azure);
                rowRange.Style.Font.Color.SetColor(Color.Black);
                rowRange.Style.ShrinkToFit = false;
                rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }

        private void AutofitValues(ExcelWorksheet worksheet)
        {
            for (int i = StartingColumn; i <= EndingColumn; i++)
            {
                worksheet.Column(i).AutoFit();
            }
        }
    }
}
