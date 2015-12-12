using Orders.com.DAL.EF.Entities;
using Orders.com.Domain;
using System.Data.Entity;

namespace Orders.com.DAL.EF
{
    public class OrdersDotComContext : DbContext
    {
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<CustomerEntity> Customers { get; set; }
		public DbSet<InventoryItemEntity> InventoryItems { get; set; }
		public DbSet<OrderEntity> Orders { get; set; }
		public DbSet<OrderItemEntity> OrderItems { get; set; }
		public DbSet<OrderStatus> OrderStatuses { get; set; }
		public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().Ignore<long>(c => c.CategoryID);
            modelBuilder.Entity<CategoryEntity>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<CustomerEntity>().Ignore<long>(c => c.CustomerID);
            modelBuilder.Entity<CustomerEntity>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<InventoryItemEntity>().Ignore<long>(c => c.InventoryItemID);
            modelBuilder.Entity<InventoryItemEntity>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<OrderEntity>().Ignore<long>(c => c.OrderID);
            modelBuilder.Entity<OrderEntity>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<OrderItemEntity>().Ignore<long>(c => c.OrderItemID);
            modelBuilder.Entity<OrderItemEntity>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<OrderStatus>().Ignore<long>(c => c.OrderStatusID);
            modelBuilder.Entity<OrderStatus>().Ignore<string>(c => c.Self);

            modelBuilder.Entity<ProductEntity>().Ignore<long>(c => c.ProductID);
            modelBuilder.Entity<ProductEntity>().Ignore<string>(c => c.Self);

            base.OnModelCreating(modelBuilder);
        }
    }
}
