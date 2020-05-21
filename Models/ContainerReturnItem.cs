using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_DetalleRetorno", Schema = "DISTRIBUCION")]
    public class ContainerReturnItem
    {
        [Key]
        [Column("IDLinea")]
        public int Id { get; set; }

        [Required]
        [Column("Familia")]
        public int ContainerFamily { get; set; }

        [Column("SKU")]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        [Column("Tamanio")]
        [MaxLength(150)]
        public string ContainerSize { get; set; }

        [Required]
        [Column("Cantidad")]
        public int ContainerQuantity { get; set; }

        [Required]
        [Column("PrecioEnvase", TypeName = "decimal(18, 4)")]
        public Decimal ContainerPrice { get; set; }

        [JsonIgnore]
        [Column("IDRetorno")]
        public int ContainerReturnId { get; set; }

        [JsonIgnore]
        public ContainerReturn ContainerReturn { get; set; }
    }
}