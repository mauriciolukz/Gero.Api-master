using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("TargetSales", Schema = "DISTRIBUCION")]
    public class TargetSale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Route { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        [MaxLength(15)]
        public string ItemFamilyCode { get; set; }

        [Required]
        public int NineLitresTarget { get; set; }

        [Required]
        [Column("TargetAmount", TypeName = "decimal(18, 4)")]
        public Decimal TargetAmount { get; set; }

        [Required]
        public int WorkingDays { get; set; }

        [Required]
        public int NineLitresTargetPerDay { get; set; }

        [Required]
        [Column("TargetAmountPerDay", TypeName = "decimal(18, 4)")]
        public Decimal TargetAmountPerDay { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}