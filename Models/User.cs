using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("Users", Schema = "DISTRIBUCION")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [NotMapped]
        public string TypedPassword { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        [DataType(DataType.Password)]
        public byte[] Password { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string InvitationToken { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset InvitationSentAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset InvitationAcceptedAt { get; set; }

        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string ResetPasswordToken { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset ResetPasswordSentAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset ResetPasswordExpiredAt { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset UpdatedAt { get; set; }

        public UserSetting Setting { get; set; }

        public UserInfo Info { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserDevice> Devices { get; set; }
    }
}
