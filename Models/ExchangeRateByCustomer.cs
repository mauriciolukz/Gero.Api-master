using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ExchangeRateByCustomers", Schema = "DISTRIBUCION")]
    public class ExchangeRateByCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

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