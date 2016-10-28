namespace Dealership.Models.Contracts.XmlSource
{
    public interface IAddress
    {
        string Street { get; }

        string ZipCode { get; }

        ICity City { get; }

        ICountry Country { get; }
    }
}