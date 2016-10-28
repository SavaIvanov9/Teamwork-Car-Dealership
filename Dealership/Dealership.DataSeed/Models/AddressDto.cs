using Dealership.Models.Contracts.XmlSource;

namespace Dealership.DataSeed.Models
{
    public class AddressDto : IAddress
    {
        public ICity City { get; set; }

        public ICountry Country { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }
}
