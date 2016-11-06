//using System.Collections.Generic;
//using System.IO;
//using System.Xml;
//using Dealership.Reports.Models.Contracts;
//using Dealership.XmlFilesProcessing.Writers.Contracts;

//namespace Dealership.XmlFilesProcessing.Writers.Common
//{
//    public class XmlDailyShopReportWriter : XmlReportWriter
//    {
//        private readonly IEnumerable<IXmlDailyShopReport> Report;
//        public XmlDailyShopReportWriter() : base()
//        {

//        }
//        public override void Write()
//        {
//            string root = "shops";
//            string shop = "shop";
//            string name = "name";
//            string order = "order";
//            string date = "date";
//            string total = "transaction";

//            if (!Directory.Exists(this.Url))
//            {
//                Directory.CreateDirectory(this.Url);
//            }

//            string fileLocation = this.Url + ReportName;

//            using (var document = XmlWriter.Create(fileLocation, this.Settings))
//            {
//                document.WriteStartDocument();
//                document.WriteStartElement(root);

//                foreach (var entity in this.Report)
//                {
//                    document.WriteStartElement(shop);
//                        document.WriteAttributeString(name, entity.ShopPlace);

//                    foreach (var ent in entity)
//                    {

//                        document.WriteStartElement(order);

//                    }
//                    document.WriteElementString(location, entity.Location);
//                    document.WriteElementString(total, entity.TotalBudget.ToString());

//                    document.WriteEndElement();
//                }

//                document.WriteEndElement();
//                document.WriteEndDocument();
//            }
//        }
//    }
//}
