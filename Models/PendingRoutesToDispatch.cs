using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    public class PendingRoutesToDispatch
    {
        [Key]
        public Int64 Id { get; set; }
        public string DeliveryDate { get; set; }
        public string WarehouseCode { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string RouteType { get; set; }
        public string DeliveryRoute { get; set; }
        public int TotalOrders { get; set; }
    }
}
