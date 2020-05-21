using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_Pedido", Schema = "DISTRIBUCION")]
    public class Order
    {
        [Key]
        [Column("IDPedido")]
        public int Id { get; set; }

        [Column("TipoPedido")]
        public int? OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        [Required]
        [Column("NumPedido")]
        [MaxLength(100)]
        public string OrderNumber { get; set; }

        [Required]
        [Column("FechaPedido")]
        public DateTime OrderDate { get; set; }

        [Column("FechaEntrega")]
        public DateTimeOffset DeliveryDate { get; set; }

        [Required]
        [Column("CodigoCliente")]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [Required]
        [Column("RutaPreventa")]
        [MaxLength(15)]
        public string PresaleRoute { get; set; }

        [Required]
        [Column("RutaEntrega")]
        [MaxLength(15)]
        public string DeliveryRoute { get; set; }

        [Required]
        [Column("FechaCarga")]
        public DateTime SynchronizationDate { get; set; }

        [Required]
        [Column("PedidoConfirmado")]
        public Boolean IsOrderConfirmed { get; set; }

        [Required]
        [Column("Finalizado")]
        public Boolean IsOrderFinished { get; set; }

        [Required]
        [Column("TotalDolares", TypeName = "decimal(18, 4)")]
        public Decimal TotalInForeignCurrency { get; set; }

        [Column("GeoReferencia")]
        public string OrderLocation { get; set; }

        [Column("Motivo")]
        public int? MotiveId { get; set; }

        [Column("Consolidado")]
        public int? ConsolidatedOrderId { get; set; }

        public List<OrderItem> Items { get; set; }

        [NotMapped]
        public virtual ContainerReturn Container { get; set; }
    }
}