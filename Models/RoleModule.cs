using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("RoleModules", Schema = "DISTRIBUCION")]
    public class RoleModule
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}