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
    public class ContainerReturnsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ContainerReturnsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ContainerReturns
        [HttpGet]
        public IEnumerable<ContainerReturn> GetContainerReturns()
        {
            return _context.ContainerReturns;
        }

        // GET: api/v1/Controllers/ContainerReturns/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContainerReturn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var containerReturn = await _context.ContainerReturns.FindAsync(id);

            if (containerReturn == null)
            {
                return NotFound();
            }

            return Ok(containerReturn);
        }

        // PUT: api/v1/Controllers/ContainerReturns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContainerReturn([FromRoute] int id, [FromBody] ContainerReturn containerReturn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != containerReturn.Id)
            {
                return BadRequest();
            }

            _context.Entry(containerReturn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerReturnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ContainerReturns.Find(id));
        }

        // POST: api/v1/Controllers/ContainerReturns
        [HttpPost]
        public async Task<IActionResult> PostContainerReturn([FromBody] ContainerReturn containerReturn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            containerReturn.SynchronizationDate = DateTime.Now;

            _context.ContainerReturns.Add(containerReturn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContainerReturn", new { id = containerReturn.Id }, containerReturn);
        }

        // DELETE: api/v1/Controllers/ContainerReturns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContainerReturn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var containerReturn = await _context.ContainerReturns.FindAsync(id);
            if (containerReturn == null)
            {
                return NotFound();
            }

            _context.ContainerReturns.Remove(containerReturn);
            await _context.SaveChangesAsync();

            return Ok(containerReturn);
        }

        private bool ContainerReturnExists(int id)
        {
            return _context.ContainerReturns.Any(e => e.Id == id);
        }
    }
}