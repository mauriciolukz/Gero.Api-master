using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API;
using Gero.API.Models;
using Gero.API.Helpers;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public WarehousesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all warehouses
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/Warehouses
        [HttpGet]
        public List<Warehouse> GetWarehouse()
        {
            List<Warehouse> warehouses = _context
                .Warehouses
                .FromSql(QueryBuilder.Build("GetWarehouses.txt", null))
                .ToList();

            if (warehouses.Any())
            {
                List<Models.Route> routes = _context
                    .Routes
                    .FromSql(QueryBuilder.Build("GetRoutes.txt", null))
                    .ToList();

                foreach (var warehouse in warehouses)
                {
                    var routesFromWarehouse = routes
                        .Where(x => x.WarehouseCode == warehouse.WarehouseCode)
                        .ToList();

                    warehouse.Routes = routesFromWarehouse;
                }

                return warehouses;
            }

            return new List<Warehouse>();
        }
    }
}