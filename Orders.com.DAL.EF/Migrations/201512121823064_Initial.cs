namespace Orders.com.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
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
                "dbo.Product",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Customer",
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
                "dbo.Order",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderItem",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.InventoryItem",
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryItem", "ProductID", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropIndex("dbo.InventoryItem", new[] { "ProductID" });
            DropIndex("dbo.OrderItem", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropTable("dbo.OrderStatus");
            DropTable("dbo.InventoryItem");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
        }
    }
}
