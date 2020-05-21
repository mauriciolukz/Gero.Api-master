using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("UserTelephones", Schema = "DISTRIBUCION")]
    public class UserTelephone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TelephoneType TelephoneType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string TelephoneNumber { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}