using Gero.API.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    [Table("DST_RegistroSincronizacion", Schema = "DISTRIBUCION")]
    public class SynchronizationRecord
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("CodigoRuta")]
        [MaxLength(10)]
        public string Route { get; set; }

        [Required]
        [Column("TipoCatalogo")]
        [MaxLength(10)]
        public string TypeOfCatalogId { get; set; }

        [Required]
        [Column("TipoVisita")]
        [MaxLength(10)]
        public string TypeOfVisitId { get; set; }

        [Required]
        [Column("NombreArchivo")]
        [MaxLength(100)]
        public string FileName { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime SynchronizationDate { get; set; }

        [Required]
        [Column("RegistrosLeidos")]
        public int RecordsRead { get; set; }

        [Required]
        [Column("RegistrosCargados1")]
        public int RecordsSynchronized1 { get; set; }

        [Required]
        [Column("RegistrosCargados2")]
        public int RecordsSynchronized2 { get; set; }

        [Column("ResultadoProceso")]
        public string ResultOfTheProcess { get; set; }

        [Column("DeviceID")]
        public string DeviceId { get; set; }

        [Required]
        [Column("Direccion")]
        public ProcessType ProcessType { get; set; }
    }
}
