using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_DetalleAbono", Schema = "DISTRIBUCION")]
    public class PaymentReceiptItem
    {
        [Key]
        [Column("IDLinea")]
        public int Id { get; set; }

        [Required]
        [Column("TipoPago")]
        [MaxLength(10)]
        public string PaymentTypeCode { get; set; }

        [Required]
        [Column("TasaCambio", TypeName = "decimal(18, 4)")]
        public Decimal ExchangeRate { get; set; }

        [Required]
        [Column("Monto", TypeName = "decimal(18, 4)")]
        public Decimal Amount { get; set; }

        [Column("MontoUSD", TypeName = "decimal(18, 4)")]
        public Decimal AmountInForeignCurrency { get; set; }

        [Column("Referencia")]
        [MaxLength(50)]
        public string BankReference { get; set; }

        [Column("Banco")]
        [MaxLength(10)]
        public string BankCode { get; set; }

        [Column("BancoSS")]
        [MaxLength(30)]
        public string LocalBankCode { get; set; }

        [Column("BancoDeposito")]
        [MaxLength(10)]
        public string DepositBank { get; set; }

        [Column("BancoDepositoBPCS")]
        [MaxLength(10)]
        public string ForeignDepositBank { get; set; }

        [Required]
        [Column("Procesado")]
        public Boolean IsPaymentProcessed { get; set; }

        [Required]
        [Column("Rechazado")]
        public Boolean IsPaymentRejected { get; set; }

        [Required]
        [Column("Anulado")]
        public Boolean IsPaymentCanceled { get; set; }

        [JsonIgnore]
        [Column("IDAbono")]
        public int PaymentReceiptId { get; set; }

        [JsonIgnore]
        public PaymentReceipt PaymentReceipt { get; set; }
    }
}