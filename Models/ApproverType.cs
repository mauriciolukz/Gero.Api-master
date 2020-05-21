using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    /// <summary>
    /// Model that represents a role for the coupon module.
    /// 
    /// Possible values:
    /// - Mobile applicant
    /// - Web applicant
    /// - Checker
    /// - Approver
    /// - Master approver
    /// </summary>
    [Table("ApproverTypes", Schema = "DISTRIBUCION")]
    public class ApproverType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string NextCode { get; set; }
    }
}
