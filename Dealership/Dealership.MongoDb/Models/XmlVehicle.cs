using System;
using System.Xml.Serialization;

namespace Dealership.MongoDb.Models
{
    [Serializable]
    [XmlType("Vehicle")]
    public class XmlVehicle
    {
        public string Type { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public string Fuel { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }
    }
}
