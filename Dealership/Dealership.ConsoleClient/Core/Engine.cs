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
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.SeedDataFromMongo);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.SeedDataFromXml);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.SeedDataFromSalesReports);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.GenerateJsonReports);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.GenerateExcelReport);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.GenerateXmlShopReport);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.GenerateXmlDailyShopReport);
            CommandProcessor.Create(textWriter).ExecuteCommand(Command.GenerateRdfAggregateDailySalesReport);
        }
    }
}
