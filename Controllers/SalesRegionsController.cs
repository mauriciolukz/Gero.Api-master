using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using System;
using Gero.API.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalesRegionsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public SalesRegionsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/SalesRegions
        [HttpGet]
        public IEnumerable<SalesRegion> GetSalesRegions()
        {
            return _context.SalesRegions;
        }

        // GET: api/v1/SalesRegions/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesRegion), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSalesRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesRegion = await _context.SalesRegions.FindAsync(id);

            if (salesRegion == null)
            {
                return NotFound();
            }

            return Ok(salesRegion);
        }

        // PUT: api/v1/SalesRegions/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutSalesRegion([FromRoute] int id, [FromBody] SalesRegion salesRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesRegion.Id)
            {
                return BadRequest();
            }

            salesRegion.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(salesRegion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesRegionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.SalesRegions.Find(id));
        }

        // POST: api/v1/SalesRegions
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostSalesRegion([FromBody] SalesRegion salesRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            salesRegion.Status = Status.Active;

            var now = DateTimeOffset.Now;

            salesRegion.CreatedAt = now;
            salesRegion.UpdatedAt = now;

            _context.SalesRegions.Add(salesRegion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesRegion", new { id = salesRegion.Id }, salesRegion);
        }

        // DELETE: api/v1/SalesRegions/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SalesRegion), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSalesRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesRegion = await _context.SalesRegions.FindAsync(id);
            if (salesRegion == null)
            {
                return NotFound(salesRegion);
            }

            _context.SalesRegions.Remove(salesRegion);
            await _context.SaveChangesAsync();

            return Ok(salesRegion);
        }

        private bool SalesRegionExists(int id)
        {
            return _context.SalesRegions.Any(e => e.Id == id);
        }
    }
}