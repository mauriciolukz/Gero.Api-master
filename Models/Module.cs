using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("Modules", Schema = "DISTRIBUCION")]
    public class Module
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
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string IconName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ClassName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string ClassType { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }

        public int? ParentId { get; set; }

        public int? OrderTypeId { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public Module Parent { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<RoleModule> Roles { get; set; }

        public virtual ICollection<Module> Children { get; set; }
    }
}