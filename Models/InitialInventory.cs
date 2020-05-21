using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("DST_InventarioInicial", Schema = "DISTRIBUCION")]
    public class InitialInventory
    {
        [Key]
        [Column("IDEncabezado")]
        public int Id { get; set; }

        [Required]
        [Column("Ruta")]
        [MaxLength(10)]
        public string Route { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }

        [Column("IDRS")]
        public int? SynchronizationId { get; set; }

        public List<InitialInventoryItem> Items { get; set; }
    }
}
