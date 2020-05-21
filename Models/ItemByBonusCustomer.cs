using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("ItemByBonusCustomers", Schema = "DISTRIBUCION")]
    public class ItemByBonusCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemCode { get; set; }

        [MaxLength(100)]
        public string ItemName { get; set; }

        [MaxLength(10)]
        public string CostCenterCode { get; set; }

        [Required]
        public int BoxesQuantity { get; set; }

        [Required]
        public int UnitsQuantity { get; set; }

        public Boolean IsForExport { get; set; }

        public Status Status { get; set; }

        public int BonusByCustomerId { get; set; }

        public BonusByCustomer BonusByCustomer { get; set; }
    }
}
