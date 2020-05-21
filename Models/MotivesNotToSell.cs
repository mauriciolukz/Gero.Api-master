using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    public class MotivesNotToSell
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoCliente")]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("CodigoRuta")]
        [MaxLength(15)]
        public string RouteCode { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime MotiveDate { get; set; }

        [Required]
        [Column("TipoVisita")]
        public int VisitType { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }

        [Required]
        [Column("MotivoNoVenta")]
        public int? MotivesNotToSellTypeId { get; set; }
        public MotivesNotToSellType MotivesNotToSellType { get; set; }
    }
}