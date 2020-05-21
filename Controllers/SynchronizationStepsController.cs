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
    public class SynchronizationStepsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public SynchronizationStepsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/SynchronizationSteps
        [HttpGet]
        public IEnumerable<SynchronizationStep> GetSynchronizationSteps()
        {
            return _context.SynchronizationSteps;
        }

        // GET: api/v1/Controllers/SynchronizationSteps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSynchronizationStep([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var synchronizationStep = await _context.SynchronizationSteps.FindAsync(id);

            if (synchronizationStep == null)
            {
                return NotFound();
            }

            return Ok(synchronizationStep);
        }

        // PUT: api/v1/Controllers/SynchronizationSteps/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSynchronizationStep([FromRoute] int id, [FromBody] SynchronizationStep synchronizationStep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != synchronizationStep.Id)
            {
                return BadRequest();
            }

            synchronizationStep.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(synchronizationStep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SynchronizationStepExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.SynchronizationSteps.Find(id));
        }

        // POST: api/v1/Controllers/SynchronizationSteps
        [HttpPost]
        public async Task<IActionResult> PostSynchronizationStep([FromBody] SynchronizationStep synchronizationStep)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTimeOffset.Now;

            synchronizationStep.CreatedAt = now;
            synchronizationStep.UpdatedAt = now;

            _context.SynchronizationSteps.Add(synchronizationStep);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSynchronizationStep", new { id = synchronizationStep.Id }, synchronizationStep);
        }

        // DELETE: api/v1/Controllers/SynchronizationSteps/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSynchronizationStep([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var synchronizationStep = await _context.SynchronizationSteps.FindAsync(id);
            if (synchronizationStep == null)
            {
                return NotFound();
            }

            _context.SynchronizationSteps.Remove(synchronizationStep);
            await _context.SaveChangesAsync();

            return Ok(synchronizationStep);
        }

        private bool SynchronizationStepExists(int id)
        {
            return _context.SynchronizationSteps.Any(e => e.Id == id);
        }
    }
}