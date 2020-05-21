using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("ApplicationVersions", Schema = "DISTRIBUCION")]
    public class ApplicationVersion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string VersionName { get; set; }

        [Required]
        [MaxLength(10)]
        public string VersionCode { get; set; }

        [Required]
        public DateTime VersionDate { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
