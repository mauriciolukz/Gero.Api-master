using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("AuthorizatonCodeTypes", Schema = "DISTRIBUCION")]
    public class AuthorizationCodeType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
