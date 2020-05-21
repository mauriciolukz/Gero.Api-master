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
    public class BonusesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public BonusesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/Bonuses
        [HttpGet]
        public IEnumerable<Bonus> GetBonuses()
        {
            return _context.Bonuses;
        }

        // GET: api/v1/Controllers/Bonuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBonus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonus = await _context.Bonuses.FindAsync(id);

            if (bonus == null)
            {
                return NotFound();
            }

            return Ok(bonus);
        }

        // PUT: api/v1/Controllers/Bonuses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBonus([FromRoute] int id, [FromBody] Bonus bonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bonus.Id)
            {
                return BadRequest();
            }

            _context.Entry(bonus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BonusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Bonuses.Find(id));
        }

        // POST: api/v1/Controllers/Bonuses
        [HttpPost]
        public async Task<IActionResult> PostBonus([FromBody] Bonus bonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bonuses.Add(bonus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBonus", new { id = bonus.Id }, bonus);
        }

        // DELETE: api/v1/Controllers/Bonuses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBonus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonus = await _context.Bonuses.FindAsync(id);
            if (bonus == null)
            {
                return NotFound();
            }

            _context.Bonuses.Remove(bonus);
            await _context.SaveChangesAsync();

            return Ok(bonus);
        }

        private bool BonusExists(int id)
        {
            return _context.Bonuses.Any(e => e.Id == id);
        }
    }
}