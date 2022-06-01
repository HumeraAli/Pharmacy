using System.Collections.Generic;

namespace Pharmacy.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        public string ShelfLetter { get; set; }       

        public ICollection<StockMedicine> Medicines { get; set; }
    }
}