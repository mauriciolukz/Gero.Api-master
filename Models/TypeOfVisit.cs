using Gero.API.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Gero.API.Models
{
    public class TypeOfVisit
    {
        [Key]
        public Int32 Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}