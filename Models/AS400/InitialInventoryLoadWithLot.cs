using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class InitialInventoryLoadWithLot
    {
        [Key]
        public Int64 Id { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitsPerBox { get; set; }
        public Decimal InitialInventory { get; set; }
        public Decimal SaleFactor { get; set; }
        public string Lot { get; set; }
    }
}