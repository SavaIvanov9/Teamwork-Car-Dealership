using System.Collections.Generic;
using System.IO;
using System.Xml;
using Dealership.Reports.Models.Contracts;
using Dealership.XmlFilesProcessing.Writers.Contracts;

namespace Dealership.XmlFilesProcessing.Writers.Common
{
    public class XmlDailyShopReportWriter : XmlReportWriter
    {
        private readonly IEnumerable<IXmlDailyShopReport> Report;
        private const string ReportName = "/XmlDailyShopReport.xml";


        public XmlDailyShopReportWriter(IEnumerable<IXmlDailyShopReport> report) : base()
        {
            this.Report = report;
        }
        public override void Write()
        {
            string root = "shops";
            string shop = "shop";
            string name = "name";
            string order = "order";
            string date = "date";
            string transaction = "transaction";

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
                    document.WriteAttributeString(name, entity.ShopPlace);

                    document.WriteStartElement(order);

                    foreach (var ent in entity.Transactions.Keys)
                    {
                        document.WriteElementString(date, ent.ToString());

                        foreach (var cash in entity.Transactions[ent])
                        {
                            document.WriteElementString(transaction, cash.ToString());
                        }

                    }

                    document.WriteEndElement();
                    document.WriteEndElement();
                }

                document.WriteEndElement();
                document.WriteEndDocument();
            }
        }
    }
}
