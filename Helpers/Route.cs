using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Helpers
{
    public class Route
    {
        private readonly DistributionContext _context;

        public Route(DistributionContext context)
        {
            _context = context;
        }

        public Models.Route GetRoute(string routeCode)
        {
            return _context
                .Routes
                .FromSql(QueryBuilder.Build("GetRoutes.txt", null))
                .ToList()
                .Find(x => x.RouteCode == routeCode);
        }

        public List<Models.Order> GetRouteOrders(string routeCode)
        {
            return _context
                .Orders
                .Include(x => x.Items)
                .Where(x => x.DeliveryRoute == routeCode)
                .Where(x => x.IsOrderConfirmed == true)
                .Where(x => x.IsOrderFinished == false)
                .Where(
                    x =>
                        x.DeliveryDate.Year == DateTime.Now.Year &&
                        x.DeliveryDate.Month == DateTime.Now.Month &&
                        x.DeliveryDate.Day == DateTime.Now.Day
                )
                .ToList();
        }
    }
}
