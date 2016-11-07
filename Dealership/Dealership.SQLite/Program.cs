using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.SQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            //test code
            using (var db = new ItemExpensesDbEntities())
            {
                var items = db.Items.ToList();

                foreach (var item in items)
                {
                    Console.WriteLine($"{item.ItemId} {item.Name} {item.Manufacturer} {item.Taxes.Substring(0, item.Taxes.Length - 1)}%");
                }
            }
        }
    }
}
