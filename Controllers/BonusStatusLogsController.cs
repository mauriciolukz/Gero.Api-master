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
    public class BonusStatusLogsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public BonusStatusLogsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/BonusStatusLogs
        [HttpGet]
        public IEnumerable<BonusStatusLog> GetBonusStatusLogs()
        {
            return _context.BonusStatusLogs;
        }

        // GET: api/v1/Controllers/BonusStatusLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBonusStatusLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonusStatusLog = await _context.BonusStatusLogs.FindAsync(id);

            if (bonusStatusLog == null)
            {
                return NotFound();
            }

            return Ok(bonusStatusLog);
        }

        // PUT: api/v1/Controllers/BonusStatusLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBonusStatusLog([FromRoute] int id, [FromBody] BonusStatusLog bonusStatusLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bonusStatusLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(bonusStatusLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BonusStatusLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/v1/Controllers/BonusStatusLogs
        [HttpPost]
        public async Task<IActionResult> PostBonusStatusLog([FromBody] BonusStatusLog bonusStatusLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BonusStatusLogs.Add(bonusStatusLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBonusStatusLog", new { id = bonusStatusLog.Id }, bonusStatusLog);
        }

        // DELETE: api/v1/Controllers/BonusStatusLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBonusStatusLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bonusStatusLog = await _context.BonusStatusLogs.FindAsync(id);
            if (bonusStatusLog == null)
            {
                return NotFound();
            }

            _context.BonusStatusLogs.Remove(bonusStatusLog);
            await _context.SaveChangesAsync();

            return Ok(bonusStatusLog);
        }

        private bool BonusStatusLogExists(int id)
        {
            return _context.BonusStatusLogs.Any(e => e.Id == id);
        }
    }
}