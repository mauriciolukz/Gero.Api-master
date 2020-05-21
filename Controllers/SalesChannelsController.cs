using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalesChannelsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public SalesChannelsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/SalesChannels
        [HttpGet]
        public IEnumerable<SalesChannel> GetSalesChannels()
        {
            return _context.SalesChannels;
        }

        // GET: api/v1/SalesChannels/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SalesChannel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetSalesChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesChannel = await _context.SalesChannels.FindAsync(id);

            if (salesChannel == null)
            {
                return NotFound();
            }

            return Ok(salesChannel);
        }

        // PUT: api/v1/SalesChannels/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutSalesChannel([FromRoute] int id, [FromBody] SalesChannel salesChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesChannel.Id)
            {
                return BadRequest();
            }

            salesChannel.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(salesChannel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesChannelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.SalesChannels.Find(id));
        }

        // POST: api/v1/SalesChannels
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostSalesChannel([FromBody] SalesChannel salesChannel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            salesChannel.Status = Status.Active;

            var now = DateTimeOffset.Now;

            salesChannel.CreatedAt = now;
            salesChannel.UpdatedAt = now;

            _context.SalesChannels.Add(salesChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalesChannel", new { id = salesChannel.Id }, salesChannel);
        }

        // DELETE: api/v1/SalesChannels/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SalesChannel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteSalesChannel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesChannel = await _context.SalesChannels.FindAsync(id);
            if (salesChannel == null)
            {
                return NotFound();
            }

            _context.SalesChannels.Remove(salesChannel);
            await _context.SaveChangesAsync();

            return Ok(salesChannel);
        }

        private bool SalesChannelExists(int id)
        {
            return _context.SalesChannels.Any(e => e.Id == id);
        }
    }
}