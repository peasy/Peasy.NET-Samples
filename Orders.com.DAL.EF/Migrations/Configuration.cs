namespace Orders.com.DAL.EF.Migrations
{
    using Domain;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Orders.com.DAL.EF.OrdersDotComContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Orders.com.DAL.EF.OrdersDotComContext context)
        {
            var customers = new List<CustomerEntity>
            {
                new CustomerEntity { Name = "Jimi Hendrix", CreatedDatetime = DateTime.Now },
                new CustomerEntity { Name = "David Gilmour", CreatedDatetime = DateTime.Now },
                new CustomerEntity { Name = "James Page", CreatedDatetime = DateTime.Now }
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
        }
    }
}
