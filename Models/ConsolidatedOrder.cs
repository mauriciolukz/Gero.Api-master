using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ConsolidatedOrders", Schema = "DISTRIBUCION")]
    public class ConsolidatedOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string RouteCode { get; set; }

        [Required]
        public int PickingId { get; set; }

        // Posible values:
        // 0 - Assigned
        // 1 - Dispatched
        // 2 - Cancelled
        [Required]
        public int OrderStatus { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}