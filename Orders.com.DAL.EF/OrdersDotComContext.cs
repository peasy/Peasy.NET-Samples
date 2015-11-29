using Orders.com.Domain;
using System.Data.Entity;

namespace Orders.com.DAL.EF
{
    public class OrdersDotComContext : DbContext
    {
		public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Ignore<long>(c => c.CustomerID);
            modelBuilder.Entity<Customer>().Ignore<string>(c => c.Self);

            base.OnModelCreating(modelBuilder);
        }
    }
}
