using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API;
using Gero.API.Models;
using Gero.API.Enumerations;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationVersionsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ApplicationVersionsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationVersions
        [HttpGet]
        public IEnumerable<ApplicationVersion> GetApplicationVersions()
        {
            return _context.ApplicationVersions;
        }

        // GET: api/ApplicationVersions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationVersion = await _context.ApplicationVersions.FindAsync(id);

            if (applicationVersion == null)
            {
                return NotFound();
            }

            return Ok(applicationVersion);
        }

        // PUT: api/ApplicationVersions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationVersion([FromRoute] int id, [FromBody] ApplicationVersion applicationVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationVersion.Id)
            {
                return BadRequest();
            }

            applicationVersion.UpdatedAt = DateTimeOffset.Now;
            _context.Entry(applicationVersion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationVersionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ApplicationVersions.Find(id));
        }

        // POST: api/ApplicationVersions
        [HttpPost]
        public async Task<IActionResult> PostApplicationVersion([FromBody] ApplicationVersion applicationVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTimeOffset.Now;

            applicationVersion.CreatedAt = now;
            applicationVersion.UpdatedAt = now;

            applicationVersion.Status = Status.Active;

            _context.ApplicationVersions.Add(applicationVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationVersion", new { id = applicationVersion.Id }, applicationVersion);
        }

        // DELETE: api/ApplicationVersions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationVersion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationVersion = await _context.ApplicationVersions.FindAsync(id);
            if (applicationVersion == null)
            {
                return NotFound();
            }

            _context.ApplicationVersions.Remove(applicationVersion);
            await _context.SaveChangesAsync();

            return Ok(applicationVersion);
        }

        /// <summary>
        /// Archive a ApplicationVersion by id
        /// </summary>
        /// <param name="id">ApplicationVersion id</param>
        /// <returns></returns>
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(ApplicationVersion), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var applicationVersions = await _context.ApplicationVersions.FindAsync(id);

            if (applicationVersions == null)
            {
                return NotFound();
            }

            applicationVersions.Status = Status.Inactive;
            applicationVersions.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(applicationVersions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(applicationVersions);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationVersionExists(applicationVersions.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Unarchive a ApplicationVersions by id
        /// </summary>
        /// <param name="id">ApplicationVersions id</param>
        /// <returns></returns>
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(ApplicationVersion), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var applicationVersions = await _context.ApplicationVersions.FindAsync(id);

            if (applicationVersions == null)
            {
                return NotFound();
            }

            applicationVersions.Status = Status.Active;
            applicationVersions.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(applicationVersions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(applicationVersions);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationVersionExists(applicationVersions.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ApplicationVersionExists(int id)
        {
            return _context.ApplicationVersions.Any(e => e.Id == id);
        }
    }
}