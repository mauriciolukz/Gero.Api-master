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
    public class RouteEventItemsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public RouteEventItemsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/RouteEventItems
        [HttpGet]
        public IEnumerable<RouteEventItem> GetRouteEventItems()
        {
            return _context.RouteEventItems;
        }

        // GET: api/v1/Controllers/RouteEventItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteEventItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeEventItem = await _context.RouteEventItems.FindAsync(id);

            if (routeEventItem == null)
            {
                return NotFound();
            }

            return Ok(routeEventItem);
        }

        // PUT: api/v1/Controllers/RouteEventItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteEventItem([FromRoute] int id, [FromBody] RouteEventItem routeEventItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeEventItem.Id)
            {
                return BadRequest();
            }

            routeEventItem.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(routeEventItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteEventItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.RouteEventItems.Find(id));
        }

        // POST: api/v1/Controllers/RouteEventItems
        [HttpPost]
        public async Task<IActionResult> PostRouteEventItem([FromBody] List<RouteEventItem> routeEventItems)
        {
            // Verify whether route event item was sent to the server
            if (routeEventItems.Any())
            {
                List<RouteEventItem> savedRouteEventItems = new List<RouteEventItem>();

                // Get current date time
                var now = DateTimeOffset.Now;

                // Iterate through all route event items sent from web/mobile application
                foreach (var routeEventItem in routeEventItems)
                {
                    // Set default values
                    routeEventItem.CreatedAt = now;
                    routeEventItem.UpdatedAt = now;

                    // Add route event item to the context
                    _context.RouteEventItems.Add(routeEventItem);

                    // Save route event item changes
                    await _context.SaveChangesAsync();

                    // Add saved route event item to the list
                    savedRouteEventItems.Add(routeEventItem);
                }

                return StatusCode(201, savedRouteEventItems);
            }
            else
            {
                return BadRequest("Route event items can not be empty");
            }
        }

        // DELETE: api/v1/Controllers/RouteEventItems/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouteEventItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeEventItem = await _context.RouteEventItems.FindAsync(id);
            if (routeEventItem == null)
            {
                return NotFound();
            }

            _context.RouteEventItems.Remove(routeEventItem);
            await _context.SaveChangesAsync();

            return Ok(routeEventItem);
        }

        private bool RouteEventItemExists(int id)
        {
            return _context.RouteEventItems.Any(e => e.Id == id);
        }
    }
}