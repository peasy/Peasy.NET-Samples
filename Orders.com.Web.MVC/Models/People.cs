using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Orders.com.Web.MVC.Models
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class OrdersContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}