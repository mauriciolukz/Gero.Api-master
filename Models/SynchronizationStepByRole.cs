using Gero.API.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("SynchronizationStepByRoles", Schema = "DISTRIBUCION")]
    public class SynchronizationStepByRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int SynchronizationStepId { get; set; }
        public SynchronizationStep SynchronizationStep { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
