using Gero.API.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("CostCenterByFamilies", Schema = "DISTRIBUCION")]
    public class CostCenterByItemFamily
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string ItemFamilyCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string CostCenterCode { get; set; }

        [Required]
        public int MaximumNumberOfBoxes { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
