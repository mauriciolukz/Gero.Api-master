using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models
{
    public class MotivesNotToSellType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}