using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ExchangeRates", Schema = "DISTRIBUCION")]
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ExchangeDate { get; set; }

        [Required]
        [Column("Rate", TypeName = "decimal(18, 4)")]
        public Decimal Rate { get; set; }

        [Required]
        public int CreationUserId { get; set; }

        public int? ModificationUserId { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}