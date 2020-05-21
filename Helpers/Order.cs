using Gero.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Helpers
{
    public class Order
    {

        List<DeliveryRoute> _deliveryRoutes = null;

        public Order(List<DeliveryRoute> deliveryRoutes)
        {
            _deliveryRoutes = deliveryRoutes;
        }

        public DateTimeOffset CalculateDeliveryDate(string deliveryRoute, DateTime synchronizationDate)
        {
            // Assign delivery date to the default synchronization date from the order
            DateTimeOffset deliveryDate = synchronizationDate.AddDays(1);

            // Verify whether the presale route is foreign or not
            if (IsForeignRoute(deliveryRoute))
            {
                // If day of week is equal to friday, let's add 3 days to become monday, otherwise, just add 2 days
                deliveryDate =
                    synchronizationDate.DayOfWeek == DayOfWeek.Friday ?
                        synchronizationDate.AddDays(3) :
                        synchronizationDate.AddDays(2);
            }

            return deliveryDate;
        }

        private bool IsForeignRoute(string deliveryRoute)
        {
            return _deliveryRoutes.Exists(x => x.RouteCode == deliveryRoute);
        }
    }
}
