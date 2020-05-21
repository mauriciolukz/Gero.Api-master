using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("Devices", Schema = "DISTRIBUCION")]
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string IMEI { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        
        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(10)]
        public string Version { get; set; }

        [Required]
        [MaxLength(50)]
        public string MacPrinter { get; set; }

        public int PhoneNumber { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public ICollection<UserDevice> Users { get; set; }
    }
}