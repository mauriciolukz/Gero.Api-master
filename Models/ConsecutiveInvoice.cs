using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_ConsecutivoFactura", Schema = "DISTRIBUCION")]
    public class ConsecutiveInvoice
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoRuta")]
        [MaxLength(10)]
        public string Route { get; set; }

        [Column("UltimaFacturaGenerada")]
        [MaxLength(30)]
        public string LastInvoiceNumber { get; set; }

        [Required]
        [Column("Secuencial")]
        public int SequenceNumber { get; set; }

        [Required]
        [Column("FechaActualizacion")]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [Column("TipoDocumento")]
        [MaxLength(5)]
        public string DocumentTypeId { get; set; }
    }
}