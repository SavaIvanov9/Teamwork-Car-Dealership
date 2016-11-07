namespace Dealership.Models.Models.SalesReportSource
{
    using System;
    using Dealership.Models.Models.XmlSource;
    using System.ComponentModel.DataAnnotations;
    using Dealership.Models.Models.MongoDbSource;
    using Contracts.SalesReportSource;
    public partial class Sale : ISale
    {
        [Key]
        public int Id { get; set; }

        public int ShopId { get; set; }
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<System.DateTime> DateOfSale { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
