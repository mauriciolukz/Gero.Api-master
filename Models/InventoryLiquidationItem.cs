using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("InventoryLiquidationItems", Schema = "DISTRIBUCION")]
    public class InventoryLiquidationItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        public int BoxesQuantity { get; set; }

        [Required]
        public int UnitsQuantity { get; set; }

        [Required]
        public int TotalOfUnits { get; set; }

        [Required]
        [Column("UnitPrice", TypeName = "decimal(18, 4)")]
        public Decimal UnitPrice { get; set; }

        [Required]
        [Column("Total", TypeName = "decimal(18, 4)")]
        public Decimal Total { get; set; }

        [JsonIgnore]
        public InventoryLiquidation InventoryLiquidation { get; set; }
    }
}
