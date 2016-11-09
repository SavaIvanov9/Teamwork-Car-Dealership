using System.Collections.Generic;

namespace Dealership.ConsoleClient.Core
{
    using System.IO;

    public class Engine
    {
        private readonly TextWriter textWriter;

        private Engine(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public static Engine Create(TextWriter textWriter)
        {
            return new Engine(textWriter);
        }

        public void Start()
        {
            var commands = new List<Command>
            {
                Command.SeedDataFromMongo,
                Command.SeedDataFromXml,
                Command.SeedDataFromSalesReports,
                Command.GenerateJsonReports,
                Command.GenerateExcelReport,
                Command.GenerateXmlShopReport,
                Command.GenerateXmlDailyShopReport,
                Command.GenerateRdfAggregateDailySalesReport
            };

            foreach (var command in commands)
            {
                CommandProcessor.Create(textWriter).ExecuteCommand(command);
            }
        }
    }
}
