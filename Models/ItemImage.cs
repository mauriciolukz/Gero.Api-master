using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ItemImages", Schema = "DISTRIBUCION")]
    public class ItemImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}