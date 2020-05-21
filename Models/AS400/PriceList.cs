using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class PriceList
    {
        [Key]
        public Int64 Id { get; set; }
        public int CorporateFather { get; set; }
        public string ItemClassCode { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemFamilyCode { get; set; }
        public string ItemFamilyName { get; set; }
        public string ItemMarkCode { get; set; }
        public string ItemMarkName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Decimal UnitPrice { get; set; }
        public int UnitsPerBox { get; set; }
        public Decimal ContainerPrice { get; set; }
        public int HasTax { get; set; }
    }
}