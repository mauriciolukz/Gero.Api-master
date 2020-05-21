using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class Item
    {
        public string ItemFamilyCode { get; set; }
        public string ItemFamilyName { get; set; }

        [Key]
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public string UnitsPerBox { get; set; }
    }
}