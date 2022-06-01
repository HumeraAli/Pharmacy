using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string ContactNumber3 { get; set; }
        public string ContactPerson { get; set; }
        public ICollection<Medicine> Medicines { get; set; }

    }
}