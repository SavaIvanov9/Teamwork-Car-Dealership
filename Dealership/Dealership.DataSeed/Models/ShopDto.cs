using Dealership.Models.Contracts.XmlSource;

namespace Dealership.DataSeed.Models
{
    public class ShopDto : IShop
    {
        public IAddress Address { get; set; }

        public string Name { get; set; }
    }
}
