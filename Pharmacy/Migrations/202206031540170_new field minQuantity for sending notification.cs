namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfieldminQuantityforsendingnotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockMedicines", "MinQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockMedicines", "MinQuantity");
        }
    }
}
