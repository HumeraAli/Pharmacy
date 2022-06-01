using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Models

{
    public class StockMedicine
    {
        [Key]
        [ForeignKey("Medicine")]
        public int Id { get; set; }
        public Medicine Medicine { get; set; }
        
        public int Quantity { get; set; }
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; }      
        
    }
}