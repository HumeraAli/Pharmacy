namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesMedicineStockMedicineSalesPurchaseShelf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MedicineType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Reciept = c.String(),
                        SupplierName = c.String(),
                        SupplierContact = c.String(),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalesDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CustomerContact = c.String(),
                        CustomerName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MedicineId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Shelves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShelfLetter = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockMedicines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ShelfId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicines", t => t.Id)
                .ForeignKey("dbo.Shelves", t => t.ShelfId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ShelfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockMedicines", "ShelfId", "dbo.Shelves");
            DropForeignKey("dbo.StockMedicines", "Id", "dbo.Medicines");
            DropForeignKey("dbo.Sales", "MedicineId", "dbo.Medicines");
            DropForeignKey("dbo.Purchases", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.StockMedicines", new[] { "ShelfId" });
            DropIndex("dbo.StockMedicines", new[] { "Id" });
            DropIndex("dbo.Sales", new[] { "MedicineId" });
            DropIndex("dbo.Purchases", new[] { "MedicineId" });
            DropTable("dbo.StockMedicines");
            DropTable("dbo.Shelves");
            DropTable("dbo.Sales");
            DropTable("dbo.Purchases");
            DropTable("dbo.Medicines");
        }
    }
}
