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
    public class BonusStatusesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public BonusStatusesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/BonusStatuses
        [HttpGet]
        public IEnumerable<BonusStatus> GetBonusTypes()
        {
            return _context.BonusStatuses;
        }

        // GET: api/v1/Controllers/BonusStatuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBonusType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonusType = await _context.BonusStatuses.FindAsync(id);

            if (bonusType == null)
            {
                return NotFound();
            }

            return Ok(bonusType);
        }

        // PUT: api/v1/Controllers/BonusStatuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBonusType([FromRoute] int id, [FromBody] BonusStatus bonusType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bonusType.Id)
            {
                return BadRequest();
            }

            _context.Entry(bonusType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BonusStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.BonusStatuses.Find(id));
        }

        // POST: api/v1/Controllers/BonusStatuses
        [HttpPost]
        public async Task<IActionResult> PostBonusType([FromBody] BonusStatus bonusType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusStatuses.Add(bonusType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBonusType", new { id = bonusType.Id }, bonusType);
        }

        // DELETE: api/v1/Controllers/BonusStatuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBonusType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonusType = await _context.BonusStatuses.FindAsync(id);
            if (bonusType == null)
            {
                return NotFound();
            }

            _context.BonusStatuses.Remove(bonusType);
            await _context.SaveChangesAsync();

            return Ok(bonusType);
        }

        private bool BonusStatusExists(int id)
        {
            return _context.BonusStatuses.Any(e => e.Id == id);
        }
    }
}