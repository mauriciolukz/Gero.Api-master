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
    [Table("Usuarios", Schema = "dbo")]
    public class DomainUser
    {
        [Key]
        [Column("USR_ID")]
        public int Id { get; set; }

        [Column("CMP_ID")]
        public int CompanyId { get; set; }

        [Column("USR_Compania_Seleccionada")]
        public int SelectedCompanyId { get; set; }

        [Column("USR_UsuarioAD")]
        public string DomainUsername { get; set; }

        [Column("USR_NombreUsuario")]
        public string Username { get; set; }

        [JsonIgnore]
        [Column("USR_Password")]
        public string Password { get; set; }

        [JsonIgnore]
        [Column("USR_Token")]
        public string AuthenticationToken { get; set; }

        [Column("USR_Nombre")]
        public string Name { get; set; }

        [Column("USR_Apellido")]
        public string Lastname { get; set; }

        [Column("USR_Email")]
        public string Email { get; set; }

        [Column("USR_Ficha")]
        public string RecordNumber { get; set; }

        [Column("USR_Cargo")]
        public string WorkPosition { get; set; }

        [Column("USR_Telefono")]
        public string TelephoneNumber { get; set; }

        [Column("USR_Extension")]
        public string TelephoneExtension { get; set; }

        [Column("USR_Celular")]
        public string CellPhoneNumber { get; set; }

        [Column("USR_UltimoAcceso")]
        public int Type { get; set; }

        [Column("USR_EstaActivo")]
        public int Status { get; set; }

        [Column("USR_UltimoAcceso")]
        public DateTime LastAuthenticationDate { get; set; }

        [Column("USR_Creado")]
        public DateTime CreatedAt { get; set; }
    }
}
