using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("Roles", Schema = "DISTRIBUCION")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public ICollection<UserRole> Users { get; set; }

        public virtual ICollection<RoleModule> Modules { get; set; }

        [NotMapped]
        public List<SynchronizationStep> SynchronizationSteps { get; set; }
    }
}