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
    public class MotivesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public MotivesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all motives
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/Motives
        [HttpGet]
        public IEnumerable<Motive> GetMotives()
        {
            return _context.Motives;
        }

        /// <summary>
        /// Query a motive by id
        /// </summary>
        /// <param name="id">Motive id</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Motives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motive = await _context.Motives.FindAsync(id);

            if (motive == null)
            {
                return NotFound();
            }

            return Ok(motive);
        }

        /// <summary>
        /// Update a motive by id
        /// </summary>
        /// <param name="id">Motive id</param>
        /// <param name="motive">Specify the motive instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Motives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotive([FromRoute] int id, [FromBody] Motive motive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motive.Id)
            {
                return BadRequest();
            }

            motive.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(motive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotiveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Motives.Find(id));
        }

        /// <summary>
        /// Create a motive
        /// </summary>
        /// <param name="motive">Specify the motive instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Motives
        [HttpPost]
        public async Task<IActionResult> PostMotive([FromBody] Motive motive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            motive.Status = Status.Active;

            var now = DateTimeOffset.Now;

            motive.CreatedAt = now;
            motive.UpdatedAt = now;

            _context.Motives.Add(motive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotive", new { id = motive.Id }, motive);
        }

        // DELETE: api/v1/Controllers/Motives/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motive = await _context.Motives.FindAsync(id);
            if (motive == null)
            {
                return NotFound();
            }

            _context.Motives.Remove(motive);
            await _context.SaveChangesAsync();

            return Ok(motive);
        }

        private bool MotiveExists(int id)
        {
            return _context.Motives.Any(e => e.Id == id);
        }
    }
}