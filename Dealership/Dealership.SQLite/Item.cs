namespace Dealership.SQLite
{

    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; }

        public decimal Expense { get; set; }
    }
}
