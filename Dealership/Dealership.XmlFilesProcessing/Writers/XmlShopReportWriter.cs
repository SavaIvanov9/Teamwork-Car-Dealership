using System.Collections.Generic;
using System.Text;
using System.Xml;
using Dealership.Reports.Models.Contracts;

namespace Dealership.XmlFilesProcessing.Writers
{
    public class XmlShopReportWriter
    {
        private const string ReportName = "XmlShopReport.xml";
        private string url;
        private XmlWriterSettings settings;

        public XmlShopReportWriter()
        {
            this.url = "../../../";
            this.settings = new XmlWriterSettings();

            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "\t";
        }

        public XmlShopReportWriter(XmlWriterSettings settings, string location)
        {
            this.settings = settings;
            this.url = location;
        }

        public void Write(IEnumerable<IXmlShopReport> report )
        {
            string root = "shops";
            string shop = "shop";      
            string name = "name";
            string location = "location";
            string total = "total-transactions";

            string fileLocation = this.url + ReportName;

            using (var document = XmlWriter.Create(fileLocation, this.settings))
            {
                document.WriteStartDocument();
                document.WriteStartElement(root);

                foreach (var entity in report)
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
