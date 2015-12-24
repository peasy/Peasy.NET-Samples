using Orders.com.BLL.Domain;
using Orders.com.DAL.EF;
using System;
using System.Collections.Generic;

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