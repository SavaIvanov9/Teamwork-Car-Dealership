using Dealership.Models.Contracts.XmlSource;

namespace Dealership.DataSeed.Models
{
    public class AddressDto : IAddress
    {
        public CityDto City { get; set; }

        public CountryDto Country { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }
}
