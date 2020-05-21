using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_Abono", Schema = "DISTRIBUCION")]
    public class PaymentReceipt
    {
        [Key]
        [Column("IDAbono")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoCliente")]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("NumRecibo")]
        [MaxLength(30)]
        public string ReceiptNumber { get; set; }

        [Column("NumReciboAnterior")]
        [MaxLength(30)]
        public string PreviousReceiptNumber { get; set; }

        [Required]
        [Column("NumDocumento")]
        [MaxLength(30)]
        public string DocumentNumber { get; set; }

        [Column("CorrelativoDeposito")]
        [MaxLength(30)]
        public string CorrelativeOfDeposit { get; set; }

        [Column("Ruta")]
        [MaxLength(10)]
        public string Route { get; set; }

        [Required]
        [Column("FechaAbono")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }

        [Column("FechaCierreRuta")]
        public DateTime DateOfRouteClosure { get; set; }
        
        [Column("TransaccionBancaria")]
        [MaxLength(50)]
        public string BankTransactionNumber { get; set; }

        [Column("IDRS")]
        public int? SynchronizationId { get; set; }

        public List<PaymentReceiptItem> Items { get; set; }
    }
}