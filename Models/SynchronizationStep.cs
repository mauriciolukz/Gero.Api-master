using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("SynchronizationSteps", Schema = "DISTRIBUCION")]
    public class SynchronizationStep
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string ApiURL { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ClassName { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}