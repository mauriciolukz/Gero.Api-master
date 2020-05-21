using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_DetalleFactura", Schema = "DISTRIBUCION")]
    public class InvoiceItem
    {
        [Key]
        [Column("IDLinea")]
        public int Id { get; set; }

        [Required]
        [Column("SKU")]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        [Column("CantidadCajas")]
        public int BoxesQuantity { get; set; }

        [Required]
        [Column("CantidadUnidades")]
        public int UnitsQuantity { get; set; }

        [Required]
        [Column("TotalUnidades")]
        public int TotalOfUnits { get; set; }

        [Required]
        [Column("MontoEnvases", TypeName = "decimal(18, 4)")]
        public Decimal ContainerAmount { get; set; }

        [Required]
        [Column("SubTotal", TypeName = "decimal(18, 4)")]
        public Decimal Subtotal { get; set; }

        [Required]
        [Column("IVA", TypeName = "decimal(18, 4)")]
        public Decimal VAT { get; set; }

        [Column("MontoCentralizacion", TypeName = "decimal(18, 4)")]
        public Decimal AmountOfCentralization { get; set; }

        [Required]
        [Column("Total", TypeName = "decimal(18, 4)")]
        public Decimal Total { get; set; }

        [JsonIgnore]
        [Column("IDFactura")]
        public int InvoiceId { get; set; }

        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}