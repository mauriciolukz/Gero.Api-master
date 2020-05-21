using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("UserSettings", Schema = "DISTRIBUCION")]
    public class UserSetting
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [MinLength(3)]
        public string Route { get; set; }

        [Required]
        public int TypeOfVisitId { get; set; }

        [NotMapped]
        public int LastInvoiceNumber { get; set; }

        [NotMapped]
        public int LastPaymentNumber { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public User User { get; set; }
    }
}
