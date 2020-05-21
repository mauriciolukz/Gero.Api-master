using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("DST_DetalleInventario", Schema = "DISTRIBUCION")]
    public class InitialInventoryItem
    {
        [Key]
        [Column("IDLinea")]
        public int Id { get; set; }

        [Required]
        [Column("SKU")]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        [Column("Lote")]
        [MaxLength(30)]
        public string Lot { get; set; }

        [Required]
        [Column("Conteo")]
        public int UnitsPerBox { get; set; }

        [Required]
        [Column("Unidades")]
        public int UnitsQuantity { get; set; }

        public int InitialInventoryId { get; set; }

        [JsonIgnore]
        public InitialInventory InitialInventory { get; set; }
    }
}
