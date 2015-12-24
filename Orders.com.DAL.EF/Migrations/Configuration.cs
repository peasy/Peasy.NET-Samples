namespace Orders.com.DAL.EF.Migrations
{
    using BLL.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Orders.com.DAL.EF.OrdersDotComContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
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
