using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class PurchaseHistory
    {
        [Key]
        public Int64 Id { get; set; }
        public string RouteCode { get; set; }
        public int CustomerCode { get; set; }
        public string ItemCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int UnitsQuantity { get; set; }
        public int BoxesQuantity { get; set; }
        public int TotalOfUnits { get; set; }
        public Int64 PurchaseNumber { get; set; }
    }
}