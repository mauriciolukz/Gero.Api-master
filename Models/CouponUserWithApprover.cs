using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("CouponUserWithApprovers", Schema = "DISTRIBUCION")]
    public class CouponUserWithApprover
    {
        [Key]
        public int Id { get; set; }

        public int ApplicantUserId { get; set; }

        [Required]
        public CouponUser ApplicantUser { get; set; }

        public int ApproverUserId { get; set; }

        [Required]
        public CouponUser ApproverUser { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
