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
    public class TypeOfVisitsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public TypeOfVisitsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/Controllers/TypeOfVisits
        [HttpGet]
        public IEnumerable<TypeOfVisit> GetTypeOfVisits()
        {
            return _context
                .TypeOfVisits
                .FromSql(QueryBuilder.Build("GetTypeOfVisits.txt", null))
                .ToList();
        }

        // GET: api/v1/Controllers/TypeOfCatalogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeOfCatalog([FromRoute] int id)
        {
            // Get list of type of visits from query
            var typeOfVisits = await _context
                .TypeOfVisits
                .FromSql(QueryBuilder.Build("GetTypeOfVisits.txt", null))
                .ToListAsync();

            // Verify whether type of visits is not empty
            if (typeOfVisits.Any())
            {
                // Find type of visit by id
                var typeOfVisit = typeOfVisits.Where(x => x.Id == id).FirstOrDefault();

                // Verify whether type of visit is not null
                if (typeOfVisit == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(typeOfVisit);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}