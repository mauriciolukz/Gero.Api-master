using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("Bonus", Schema = "DISTRIBUCION")]
    public class Bonus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TypeOfRequest TypeOfRequest { get; set; }

        [Required]
        public int ApplicantUserId { get; set; }

        public int OrderTypeId { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        public int MotiveId { get; set; }

        [Required]
        public Motive Motive { get; set; }

        public int? CheckerUserId { get; set; }

        [Required]
        public int ApproverUserId { get; set; }

        public int? MasterApproverUserId { get; set; }

        public int BonusStatusId { get; set; }

        [Required]
        public BonusStatus BonusStatus { get; set; }

        public List<BonusByCustomer> Customers { get; set; }
    }
}
