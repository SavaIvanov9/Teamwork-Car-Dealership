namespace Dealership.Reports.Models.Contracts
{
    public interface IXmlShopReport
    {
        string ShopPlace { get; set; }

        decimal TotalBudget { get; set; }
    }
}