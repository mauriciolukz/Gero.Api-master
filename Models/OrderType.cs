using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    [Table("OrderTypes", Schema = "DISTRIBUCION")]
    public class OrderType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        public int? ParentId { get; set; }

        [JsonIgnore]
        public OrderType Parent { get; set; }

        [NotMapped]
        public virtual ICollection<Motive> Motives { get; set; }
    }
}
