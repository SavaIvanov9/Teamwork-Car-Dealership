using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Dealership.Reports.Models.Contracts;
using Dealership.XmlFilesProcessing.Writers.Contracts;

namespace Dealership.XmlFilesProcessing.Writers.Common
{
    public class XmlShopReportWriter : IXmlShopReportWriter
    {
        private const string ReportName = "/XmlShopReport.xml";
        private readonly string Url;
        private readonly XmlWriterSettings Settings;
        private readonly IEnumerable<IXmlShopReport> Report;

        public XmlShopReportWriter(IEnumerable<IXmlShopReport> report)
        {
            this.Report = report;

            this.Url = "../../../../Xml-Reports";
            this.Settings = new XmlWriterSettings();

            Settings.Encoding = Encoding.UTF8;
            Settings.Indent = true;
            Settings.IndentChars = "\t";
        }

        public void Write()
        {
            string root = "shops";
            string shop = "shop";      
            string name = "name";
            string location = "location";
            string total = "total-transactions";

            if (!Directory.Exists(this.Url))
            {
                Directory.CreateDirectory(this.Url);
            }

            string fileLocation = this.Url + ReportName;

            using (var document = XmlWriter.Create(fileLocation, this.Settings))
            {
                document.WriteStartDocument();
                document.WriteStartElement(root);

                foreach (var entity in this.Report)
                {
                    document.WriteStartElement(shop);

                    document.WriteElementString(name, entity.ShopPlace);
                    document.WriteElementString(location, entity.Location);
                    document.WriteElementString(total, entity.TotalBudget.ToString());

                    document.WriteEndElement();
                }

                document.WriteEndElement();
                document.WriteEndDocument();
            }
        }
    }
}
