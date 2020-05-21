using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_Factura", Schema = "DISTRIBUCION")]
    public class Invoice
    {
        [Key]
        [Column("IDFactura")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoCliente")]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("NumFactura")]
        [MaxLength(30)]
        public string InvoiceNumber { get; set; }

        [Column("NumFacturaBPCS")]
        [MaxLength(30)]
        public string ForeignInvoiceNumber { get; set; }

        [Column("FacturaManual")]
        [MaxLength(30)]
        public string ManualInvoiceNumber { get; set; }

        [Required]
        [Column("NumPedido")]
        [MaxLength(30)]
        public string OrderNumber { get; set; }

        [Column("NumPedidoBPCS")]
        [MaxLength(30)]
        public string ForeignOrderNumber { get; set; }

        [Required]
        [Column("FechaFactura")]
        public DateTime InvoiceDate { get; set; }

        [Column("FechaPedido")]
        public DateTime OrderDate { get; set; }

        [Column("Ruta")]
        [MaxLength(15)]
        public string Route { get; set; }

        [Required]
        [Column("SubTotal", TypeName = "decimal(18, 4)")]
        public Decimal Subtotal { get; set; }

        [Required]
        [Column("IVA", TypeName = "decimal(18, 4)")]
        public Decimal VAT { get; set; }

        [Required]
        [Column("Total", TypeName = "decimal(18, 4)")]
        public Decimal Total { get; set; }

        [Column("Contado")]
        public Boolean IsCashInvoice { get; set; }

        [Column("ContadoSS")]
        public Boolean IsCashForeignInvoice { get; set; }

        [Required]
        [Column("Procesado")]
        public Boolean IsInvoiceFinished { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }
        
        [Column("IDRS")]
        public int? SynchronizationId { get; set; }

        [Column("PorcenCentralizacion", TypeName = "decimal(18, 4)")]
        public Decimal? PercentageOfCentralization { get; set; }

        [Column("MontoCentralizacion", TypeName = "decimal(18, 4)")]
        public Decimal? AmountOfCentralization { get; set; }

        [Column("Anulado")]
        public Boolean IsInvoiceCanceled { get; set; }

        [Required]
        [Column("TotalDolares", TypeName = "decimal(18, 4)")]
        public Decimal TotalInForeignCurrency { get; set; }

        public List<InvoiceItem> Items { get; set; }

        [NotMapped]
        public virtual AuthorizationCode AuthorizationCode { get; set; }
    }
}