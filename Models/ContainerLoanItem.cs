using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ContainerLoanItems", Schema = "DISTRIBUCION")]
    public class ContainerLoanItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [Required]
        public int BoxesQuantity { get; set; }

        [Required]
        public int UnitsPerBox { get; set; }

        [Required]
        public int UnitsQuantity { get; set; }

        [Required]
        public int TotalOfUnits { get; set; }

        [Required]
        [Column("UnitPrice", TypeName = "decimal(18, 4)")]
        public Decimal UnitPrice { get; set; }

        [Required]
        [Column("Subtotal", TypeName = "decimal(18, 4)")]
        public Decimal Subtotal { get; set; }

        [Required]
        [Column("VAT", TypeName = "decimal(18, 4)")]
        public Decimal VAT { get; set; }

        [Required]
        [Column("Total", TypeName = "decimal(18, 4)")]
        public Decimal Total { get; set; }

        // Posible values:
        // 0 - Pending
        // 1 - Invoiced
        // 2 - Returned
        [Required]
        public int LoanItemStatus { get; set; }

        [JsonIgnore]
        public int ContainerLoanId { get; set; }

        [JsonIgnore]
        public ContainerLoan ContainerLoan { get; set; }
    }
}