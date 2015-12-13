namespace Orders.com.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        QuantityOnHand = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductID = c.Long(nullable: false),
                        Version = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BackorderedDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        SubmittedDate = c.DateTime(),
                        OrderStatusID = c.Long(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CustomerID = c.Long(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryID = c.Long(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDatetime = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(),
                        LastModifiedDatetime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
        }
    }
}
