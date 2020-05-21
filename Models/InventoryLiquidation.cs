using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("InventoryLiquidations", Schema = "DISTRIBUCION")]
    public class InventoryLiquidation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Route { get; set; }

        [Required]
        public DateTimeOffset SynchronizationDate { get; set; }

        public List<InventoryLiquidationItem> Items { get; set; }
    }
}
