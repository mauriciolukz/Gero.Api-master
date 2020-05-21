using Gero.API.Enumerations;
using Gero.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("BonusByCustomers", Schema = "DISTRIBUCION")]
    public class BonusByCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string CustomerCode { get; set; }

        [MaxLength(250)]
        public string CustomerName { get; set; }

        [MaxLength(250)]
        public string BusinessName { get; set; }

        [MaxLength(250)]
        public string CustomerTypeName { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTimeOffset DispatchDate { get; set; }

        public string MotiveDescription { get; set; }

        public Status Status { get; set; }

        public Bonus Bonus { get; set; }

        public List<ItemByBonusCustomer> Items { get; set; }
    }
}