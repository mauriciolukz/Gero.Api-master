using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RouteSummariesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public RouteSummariesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/RouteSummaries
        [HttpGet]
        public IEnumerable<RouteSummary> GetRouteSummaries()
        {
            return _context.RouteSummaries;
        }

        // GET: api/v1/Controllers/RouteSummaries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteSummary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeSummary = await _context.RouteSummaries.FindAsync(id);

            if (routeSummary == null)
            {
                return NotFound();
            }

            return Ok(routeSummary);
        }

        // PUT: api/v1/Controllers/RouteSummaries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteSummary([FromRoute] int id, [FromBody] RouteSummary routeSummary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeSummary.Id)
            {
                return BadRequest();
            }

            _context.Entry(routeSummary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteSummaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.RouteSummaries.Find(id));
        }

        // POST: api/v1/Controllers/RouteSummaries
        [HttpPost]
        public async Task<IActionResult> PostRouteSummary([FromBody] RouteSummary routeSummary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RouteSummaries.Add(routeSummary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRouteSummary", new { id = routeSummary.Id }, routeSummary);
        }

        // DELETE: api/v1/Controllers/RouteSummaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouteSummary([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeSummary = await _context.RouteSummaries.FindAsync(id);
            if (routeSummary == null)
            {
                return NotFound();
            }

            _context.RouteSummaries.Remove(routeSummary);
            await _context.SaveChangesAsync();

            return Ok(routeSummary);
        }

        private bool RouteSummaryExists(int id)
        {
            return _context.RouteSummaries.Any(e => e.Id == id);
        }
    }
}