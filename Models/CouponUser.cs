using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("CouponUsers", Schema = "DISTRIBUCION")]
    public class CouponUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public int ApproverTypeId { get; set; }
        public ApproverType ApproverType { get; set; }
    }
}
