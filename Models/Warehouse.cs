using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models
{
    public class Warehouse
    {
        [Key]
        public string WarehouseCode { get; set; }

        public string WarehouseName { get; set; }

        public List<Route> Routes { get; set; }
    }
}