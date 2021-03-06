﻿using System.Collections.Generic;
using System.IO;
using System.Xml;
using Dealership.Reports.Models.Contracts;

namespace Dealership.XmlFilesProcessing.Writers.Common
{
    public class XmlDailyShopReportWriter : XmlReportWriter
    {
        private const string ReportName = "/XmlDailyShopReport.xml";
        private readonly string Url;
        private readonly IEnumerable<IXmlDailyShopReport> Report;


        public XmlDailyShopReportWriter(IEnumerable<IXmlDailyShopReport> report, string url = DefaultUrl)
            : base()
        {
            this.Url = url;
            this.Report = report;
        }
        public override void Write()
        {
            string root = "shops";
            string shop = "shop";
            string name = "name";
            string orders = "orders";
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

                    document.WriteStartElement(orders);

                    foreach (var ent in entity.Transactions.Keys)
                    {
                        document.WriteStartElement(date, ent.ToString());

                        foreach (var cash in entity.Transactions[ent])
                        {
                            document.WriteElementString(transaction, cash.ToString());
                        }

                        document.WriteEndElement();
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
