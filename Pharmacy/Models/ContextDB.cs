using System;
using System.Data.Entity;
using System.Linq;

namespace Pharmacy.Models
{
    public class ContextDB : DbContext
    {
        // Your context has been configured to use a 'ContextDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Pharmacy.ContextDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ContextDB' 
        // connection string in the application configuration file.
        public ContextDB()
            : base("name=ContextDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<StockMedicine> StockMedicines { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Shelf> Shelves { get; set; }

        public virtual DbSet<Sales> Sales { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}