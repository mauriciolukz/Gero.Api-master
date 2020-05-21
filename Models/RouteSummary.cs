using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    [Table("DST_ResumenRuta", Schema = "DISTRIBUCION")]
    public class RouteSummary
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoRuta")]
        [MaxLength(10)]
        public string RouteCode { get; set; }

        [Required]
        [Column("TipoVisita")]
        [MaxLength(10)]
        public string TypeOfVisit { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime DateOfVisit { get; set; }

        [Required]
        [Column("KMInicial")]
        public int InitialMileage { get; set; }

        [Required]
        [Column("KMRecorrido")]
        public int KilometersTraveled { get; set; }

        [Required]
        [Column("KMFinal")]
        public int FinalMileage { get; set; }

        [Required]
        [Column("NegociosVisitados")]
        public int VisitedBusinessQuantity { get; set; }

        [Required]
        [Column("NegociosEfectivos")]
        public int EffectiveBusinessQuantity { get; set; }

        [Required]
        [Column("NoVenta")]
        public int NotEffectiveBusinessQuantity { get; set; }

        [Required]
        [Column("MontoTotalVentas", TypeName = "decimal(18, 4)")]
        public Decimal AmountSold { get; set; }

        [Required]
        [Column("MontoTotalDeposito", TypeName = "decimal(18, 4)")]
        public Decimal AmountDeposited { get; set; }

        [Required]
        [Column("MontoTotalMinutas", TypeName = "decimal(18, 4)")]
        public Decimal AmountInBankMinute { get; set; }

        [Required]
        [Column("MontoTotalPedidos", TypeName = "decimal(18, 4)")]
        public Decimal TotalAmount { get; set; }
    }
}