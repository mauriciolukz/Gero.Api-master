using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("ItemCentralizationByCustomers", Schema = "DISTRIBUCION")]
    public class ItemCentralizationByCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerCode { get; set; }

        [Required]
        public string ItemCode { get; set; }

        [Required]
        [Column("PercentageOfCentralization", TypeName = "decimal(18, 4)")]
        public Decimal PercentageOfCentralization { get; set; }
    }
}