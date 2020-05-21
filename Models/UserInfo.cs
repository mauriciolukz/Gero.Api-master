using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("UserInfos", Schema = "DISTRIBUCION")]
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string IDNumber { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string LicenseNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset Birthdate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string NationalityCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string NationalityName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string DepartmentCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string DepartmentName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10)]
        public string MunicipalityCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string MunicipalityName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(200)]
        public string Address { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public User User { get; set; }

        public List<UserTelephone> Phones { get; set; }
    }
}