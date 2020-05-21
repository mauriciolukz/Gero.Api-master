using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("BonusStatuses", Schema = "DISTRIBUCION")]
    public class BonusStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? PreviousBonusStatusId { get; set; }
        public BonusStatus PreviousBonusStatus { get; set; }

        public int? NextBonusStatusId { get; set; }
        public BonusStatus NextBonusStatus { get; set; }

        public Status Status { get; set; }
    }
}
