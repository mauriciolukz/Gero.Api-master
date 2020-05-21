using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ModulesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all modules
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/Modules
        [HttpGet]
        public IEnumerable<Module> GetModules()
        {
            return _context.Modules;
        }

        /// <summary>
        /// Query a module by id
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Modules/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Module), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetModule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @module = await _context
                .Modules
                .Include(x => x.Parent)
                .Include(x => x.Children)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (@module == null)
            {
                return NotFound();
            }

            return Ok(@module);
        }

        /// <summary>
        /// Update a module by id
        /// </summary>
        /// <param name="id">Module id</param>
        /// <param name="module">Specify the module instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Modules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule([FromRoute] int id, [FromBody] Module @module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @module.Id)
            {
                return BadRequest();
            }

            @module.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Modules.Find(id));
        }

        /// <summary>
        /// Create a module
        /// </summary>
        /// <param name="module">Specify the module instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Modules
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostModule([FromBody] Module @module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            @module.Status = Status.Active;

            var now = DateTimeOffset.Now;

            @module.CreatedAt = now;
            @module.UpdatedAt = now;

            _context.Modules.Add(@module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/v1/Controllers/Modules/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Modules.Remove(@module);
            await _context.SaveChangesAsync();

            return Ok(@module);
        }

        /// <summary>
        /// Archive a module by id
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Modules/5/Archive
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(Module), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var module = await _context.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            module.Status = Status.Inactive;
            module.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(module);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(module.Id))
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
        /// Unarchive a module by id
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Modules/5/Unarchive
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var module = await _context.Modules.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            module.Status = Status.Active;
            module.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(module);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(module.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}