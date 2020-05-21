using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("RouteEventItems", Schema = "DISTRIBUCION")]
    public class RouteEventItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(150)]
        public string ActionName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ActionDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string EntityName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string EntityId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }

        public int RouteEventId { get; set; }

        [JsonIgnore]
        public RouteEvent RouteEvent { get; set; }
    }
}