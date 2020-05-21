using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("ContainerLoans", Schema = "DISTRIBUCION")]
    public class ContainerLoan
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string OrderNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(15)]
        public string PresaleRoute { get; set; }

        [Required]
        [MaxLength(15)]
        public string DeliveryRoute { get; set; }

        [Required]
        public DateTimeOffset LoadDate { get; set; }

        [Required]
        public DateTimeOffset DueDate { get; set; }

        [Required]
        public DateTimeOffset SynchronizationDate { get; set; }

        public List<ContainerLoanItem> Items { get; set; }
    }
}