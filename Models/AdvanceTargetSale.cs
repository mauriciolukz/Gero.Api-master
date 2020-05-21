using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    public class AdvanceTargetSale
    {
        [Key]
        public Int64 Id { get; set; }
        public string RouteCode { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public String ItemFamilyCode { get; set; }
        public String ItemFamily { get; set; }
        public int NineLitresTarget { get; set; }
        [Column("TargetAmount", TypeName = "decimal(18, 4)")]
        public Decimal TargetAmount { get; set; }
        public int nineLitresTargetPerDay { get; set; }
        [Column("TargetAmountPerDay", TypeName = "decimal(18, 4)")]
        public Decimal TargetAmountPerDay { get; set; }
        public Double TotalCase { get; set; }
        public Double TotalNineLitreCase { get; set; }
        public int PercentAdvanceNineLitreCase { get; set; }
        [Column("TotalAmount", TypeName = "decimal(18, 4)")]
        public Decimal TotalAmount { get; set; }
        public int PercentAdvanceAmount { get; set; }
    }
}