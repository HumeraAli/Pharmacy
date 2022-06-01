using System;

namespace Pharmacy.Models
{
    
    public class Purchase
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public string Reciept { get; set; }
        public string SupplierName { get; set; }
        public string SupplierContact { get; set; }
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
        
    }
}