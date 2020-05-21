using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("BonusStatusLogs", Schema = "DISTRIBUCION")]
    public class BonusStatusLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CurrentStatus { get; set; }

        public int? NextStatus { get; set; }

        [Required]
        public int UserId { get; set; }

        public int BonusId { get; set; }

        [JsonIgnore]
        public Bonus Bonus { get; set; }
    }
}
