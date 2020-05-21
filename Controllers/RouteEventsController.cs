using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RouteEventsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public RouteEventsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/RouteEvents
        [HttpGet]
        public IEnumerable<RouteEvent> GetRouteEvents()
        {
            return _context.RouteEvents;
        }

        // GET: api/v1/Controllers/RouteEvents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRouteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeEvent = await _context.RouteEvents.FindAsync(id);

            if (routeEvent == null)
            {
                return NotFound();
            }

            return Ok(routeEvent);
        }

        // PUT: api/v1/Controllers/RouteEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteEvent([FromRoute] int id, [FromBody] RouteEvent routeEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != routeEvent.Id)
            {
                return BadRequest();
            }

            routeEvent.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(routeEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteEventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.RouteEvents.Find(id));
        }

        // POST: api/v1/Controllers/RouteEvents
        [HttpPost]
        public async Task<IActionResult> PostRouteEvent([FromBody] RouteEvent routeEvent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            routeEvent.Status = Status.Active;

            var now = DateTimeOffset.Now;

            routeEvent.CreatedAt = now;
            routeEvent.UpdatedAt = now;

            _context.RouteEvents.Add(routeEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRouteEvent", new { id = routeEvent.Id }, routeEvent);
        }

        // DELETE: api/v1/Controllers/RouteEvents/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRouteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var routeEvent = await _context.RouteEvents.FindAsync(id);
            if (routeEvent == null)
            {
                return NotFound();
            }

            _context.RouteEvents.Remove(routeEvent);
            await _context.SaveChangesAsync();

            return Ok(routeEvent);
        }

        private bool RouteEventExists(int id)
        {
            return _context.RouteEvents.Any(e => e.Id == id);
        }
    }
}