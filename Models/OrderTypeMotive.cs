using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("OrderTypeMotives", Schema = "DISTRIBUCION")]
    public class OrderTypeMotive
    {
        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        public int MotiveId { get; set; }
        public Motive Motive { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}