using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Dealership.Reports.Models.Contracts;
using Dealership.XmlFilesProcessing.Writers.Contracts;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Dealership.XmlFilesProcessing.Writers.Common
{
    public class PdfAggregatedDailySalesReportWriter : IReportWriter
    {
        private const string ReportName = "/PdfAggregateDailySalesReport.pdf";
        private readonly string Url;
        private readonly IEnumerable<IPdfAggregatedDailySalesReport> Report;


        public PdfAggregatedDailySalesReportWriter(IEnumerable<IPdfAggregatedDailySalesReport> Report, string url = "../../../../Pdf-Reports") : base()
        {
            this.Url = url;
            this.Report = Report;
        }
        public virtual void Write()
        {
            string path = this.Url + ReportName;

            if (!Directory.Exists(this.Url))
            {
                Directory.CreateDirectory(this.Url);
            }

            using (FileStream memoryStream = new FileStream(path, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                const int cols = 5;
                PdfPTable table = new PdfPTable(cols);

                foreach (var dailyReport in Report)
                {
                    PdfPCell day = new PdfPCell(new Phrase(dailyReport.Date.ToString()));
                    day.Colspan = cols;
                    day.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(day);

                    foreach (var entity in dailyReport.DailyEntities)
                    {
                        table.AddCell(entity.Brand);
                        table.AddCell(entity.Model);
                        table.AddCell(entity.Quantity.ToString());
                        table.AddCell($"{entity.UnitPrice:F2}");
                        table.AddCell($"{entity.TotalPrice:F2}");
                    }

                    PdfPCell totalSum = new PdfPCell(new Phrase(dailyReport.TotalDailySales.ToString()));
                    day.Colspan = cols;
                    day.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    table.AddCell(day);
                }

                document.Add(table);

                document.Close();
            }
        }
    }
}
