using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Helpers;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TypeOfCatalogsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public TypeOfCatalogsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/TypeOfCatalogs
        [HttpGet]
        public IEnumerable<TypeOfCatalog> GetTypeOfCatalogs()
        {
            return _context
                .TypeOfCatalogs
                .FromSql(QueryBuilder.Build("GetTypeOfCatalogs.txt", null))
                .ToList();
        }

        // GET: api/v1/Controllers/TypeOfCatalogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeOfCatalog([FromRoute] int id)
        {
            // Get list of type of catalogs from query
            var typeOfCatalogs = await _context
                .TypeOfCatalogs
                .FromSql(QueryBuilder.Build("GetTypeOfCatalogs.txt", null))
                .ToListAsync();

            // Verify whether type of catalogs is not empty
            if (typeOfCatalogs.Any())
            {
                // Find type of catalog by id
                var typeOfCatalog = typeOfCatalogs.Where(x => x.Id == id).FirstOrDefault();

                // Verify whether type of catalog is not null
                if (typeOfCatalog == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(typeOfCatalog);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}