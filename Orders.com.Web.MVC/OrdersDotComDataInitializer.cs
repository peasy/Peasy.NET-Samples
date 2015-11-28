using Orders.com.DAL.EF;
using Orders.com.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC
{
   public class OrdersDotComDataInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OrdersDotComContext>
    {
        protected override void Seed(OrdersDotComContext context)
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Jimi Hendrix", CreatedDatetime = DateTime.Now },
                new Customer { Name = "David Gilmour", CreatedDatetime = DateTime.Now },
                new Customer { Name = "James Page", CreatedDatetime = DateTime.Now }
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
        }
    }
}