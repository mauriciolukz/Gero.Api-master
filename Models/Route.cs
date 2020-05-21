using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    public class Route
    {
        [Key]
        public string RouteCode { get; set; }

        public string RouteName { get; set; }

        public string RouteType { get; set; }

        [JsonIgnore]
        public string WarehouseCode { get; set; }
    }
}