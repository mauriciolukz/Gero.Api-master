using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Models
{
    public class Dispatch
    {
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        public int? OrderTypeId { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string RouteCode { get; set; }

    }
}
