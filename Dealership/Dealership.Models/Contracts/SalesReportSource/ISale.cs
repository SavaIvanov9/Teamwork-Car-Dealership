using System.Collections.Generic;
using System;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;

namespace Dealership.Models.Contracts.SalesReportSource
{
    public interface ISale : IEntity
    {
        int ShopId { get; set; }
        int VehicleId { get; set; }
        int EmployeeId { get; set; }
        Nullable<int> Quantity { get; set; }
        Nullable<decimal> Price { get; set; }
        Nullable<System.DateTime> DateOfSale { get; set; }

        Shop Shop { get; set; }
        Vehicle Vehicle { get; set; }
        Employee Employee { get; set; }
    }
}
