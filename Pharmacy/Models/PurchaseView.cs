using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class PurchaseView                                       
    {
        public Purchase Purchase { get; set; }
        public string ShelfLetter { get; set; }
        public string MedicineName { get; set; }
    }
}