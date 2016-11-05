namespace Dealership.Reports.Models.Contracts
{
    public interface IXmlShopReport
    {
        string ShopPlace { get; set; }

        string Location { get; set; }

        decimal? TotalBudget { get; set; }
    }
}