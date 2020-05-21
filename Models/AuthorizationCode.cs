using Gero.API.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("AuthorizationCodes", Schema = "DISTRIBUCION")]
    public class AuthorizationCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(8)]
        public string Code { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MinLength(2)]
        public string Route { get; set; }

        [MaxLength(30)]
        public String CustomerCode { get; set; }

        public int? AuthorizationCodeTypeId { get; set; }
        public AuthorizationCodeType AuthorizationCodeType { get; set; }

        public DateTimeOffset? Date { get; set; }

        [Required]
        public Status Status { get; set; }

        [MaxLength(50)]
        public string Entity { get; set; }

        public int? EntityId { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
