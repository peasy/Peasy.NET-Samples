namespace Orders.com.DAL.EF.Migrations
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Orders.com.DAL.EF.OrdersDotComContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Orders.com.DAL.EF.OrdersDotComContext";
        }

        protected override void Seed(Orders.com.DAL.EF.OrdersDotComContext context)
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
