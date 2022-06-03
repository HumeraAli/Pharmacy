namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctrelationshipfrommedicinetostock : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "MedicineId", "dbo.Medicines");
            DropIndex("dbo.Sales", new[] { "MedicineId" });
            AddColumn("dbo.Sales", "StockMedicineId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sales", "StockMedicineId");
            AddForeignKey("dbo.Sales", "StockMedicineId", "dbo.StockMedicines", "Id", cascadeDelete: true);
            DropColumn("dbo.Sales", "MedicineId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "MedicineId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sales", "StockMedicineId", "dbo.StockMedicines");
            DropIndex("dbo.Sales", new[] { "StockMedicineId" });
            DropColumn("dbo.Sales", "StockMedicineId");
            CreateIndex("dbo.Sales", "MedicineId");
            AddForeignKey("dbo.Sales", "MedicineId", "dbo.Medicines", "Id", cascadeDelete: true);
        }
    }
}
