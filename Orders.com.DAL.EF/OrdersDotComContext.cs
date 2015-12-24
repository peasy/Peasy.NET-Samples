using Orders.com.BLL.Domain;
using System.Data.Entity;

namespace Orders.com.DAL.EF
{
    public class OrdersDotComContext : DbContext
    {
		public DbSet<Category> Categories { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<InventoryItem> InventoryItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<OrderStatus> OrderStatuses { get; set; }
		public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Ignore<long>(c => c.CategoryID);
            modelBuilder.Entity<Category>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<Customer>().Ignore<long>(c => c.CustomerID);
            modelBuilder.Entity<Customer>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<InventoryItem>().Ignore<long>(c => c.InventoryItemID);
            modelBuilder.Entity<InventoryItem>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<Order>().Ignore<long>(c => c.OrderID);
            modelBuilder.Entity<Order>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<OrderItem>().Ignore<long>(c => c.OrderItemID);
            modelBuilder.Entity<OrderItem>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<OrderStatus>().Ignore<long>(c => c.OrderStatusID);
            modelBuilder.Entity<OrderStatus>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<Product>().Ignore<long>(c => c.ProductID);
            modelBuilder.Entity<Product>().Ignore<string>(c => c.Self);

            base.OnModelCreating(modelBuilder);
        }
    }
}
