using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_RetornoEnvase", Schema = "DISTRIBUCION")]
    public class ContainerReturn
    {
        [Key]
        [Column("IDRetorno")]
        public int Id { get; set; }

        [Required]
        [Column("NumRetorno")]
        [MaxLength(30)]
        public string ReturnNumber { get; set; }

        [Column("FechaRetorno")]
        public DateTime ReturnDate { get; set; }

        [Column("CodigoCliente")]
        [MaxLength(10)]
        public string CustomerCode { get; set; }

        [Column("RutaPreventa")]
        [MaxLength(15)]
        public string PresaleRoute { get; set; }

        [Column("RutaEntrega")]
        [MaxLength(15)]
        public string DeliveryRoute { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }

        [Required]
        [Column("MontoDevolucion", TypeName = "decimal(18, 4)")]
        public Decimal Amount { get; set; }

        [Required]
        [Column("Finalizado")]
        public Boolean IsReturnFinished { get; set; }

        [Required]
        [Column("Preventa")]
        public Boolean IsPresale { get; set; }

        public List<ContainerReturnItem> Items { get; set; }
    }
}