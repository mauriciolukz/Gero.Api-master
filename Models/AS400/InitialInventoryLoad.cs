using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class InitialInventoryLoad
    {
        [Key]
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitsPerBox { get; set; }
        public Decimal InitialInventory { get; set; }
        public Decimal SaleFactor { get; set; }
    }
}