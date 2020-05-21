using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    public class CouponUserByApproverType
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string JobTitle { get; set; }
        public string ApproverType { get; set; }
        [NotMapped]
        public List<CouponUserByApproverType> Approvers { get; set; }
    }
}
