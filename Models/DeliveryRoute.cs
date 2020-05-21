using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("DST_RutaEntrega_48", Schema = "DISTRIBUCION")]
    public class DeliveryRoute
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoRuta")]
        [MaxLength(10)]
        public string RouteCode { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime CreatedAt { get; set; }
    }
}
