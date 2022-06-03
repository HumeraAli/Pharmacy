using System;

namespace Pharmacy.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public int Quantity { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerName { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public int StockMedicineId { get; set; }
        public StockMedicine StockMedicine { get; set; }
        }
}