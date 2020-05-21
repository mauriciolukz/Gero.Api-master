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
    public class AuthorizationCodesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public AuthorizationCodesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/AuthorizationCodes
        [HttpGet]
        public IEnumerable<AuthorizationCode> GetAuthorizationCodes()
        {
            return _context.AuthorizationCodes;
        }

        // GET: api/AuthorizationCodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorizationCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorizationCode = await _context.AuthorizationCodes.FindAsync(id);

            if (authorizationCode == null)
            {
                return NotFound();
            }

            return Ok(authorizationCode);
        }

        // PUT: api/AuthorizationCodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorizationCode([FromRoute] int id, [FromBody] AuthorizationCode authorizationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authorizationCode.Id)
            {
                return BadRequest();
            }

            authorizationCode.UpdatedAt = DateTimeOffset.Now;
            _context.Entry(authorizationCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.AuthorizationCodes.Find(id));
        }

        // POST: api/AuthorizationCodes
        [HttpPost]
        public async Task<IActionResult> PostAuthorizationCode([FromBody] AuthorizationCode authorizationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTimeOffset.Now;

            authorizationCode.CreatedAt = now;
            authorizationCode.UpdatedAt = now;

            authorizationCode.Status = Status.Active;

            _context.AuthorizationCodes.Add(authorizationCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorizationCode", new { id = authorizationCode.Id }, authorizationCode);
        }

        // DELETE: api/AuthorizationCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorizationCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorizationCode = await _context.AuthorizationCodes.FindAsync(id);
            if (authorizationCode == null)
            {
                return NotFound();
            }

            _context.AuthorizationCodes.Remove(authorizationCode);
            await _context.SaveChangesAsync();

            return Ok(authorizationCode);
        }

        /// <summary>
        /// Archive a AuthorizationCode by id
        /// </summary>
        /// <param name="id">AuthorizationCode id</param>
        /// <returns></returns>
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(AuthorizationCode), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var authorizationCodes = await _context.AuthorizationCodes.FindAsync(id);

            if (authorizationCodes == null)
            {
                return NotFound();
            }

            authorizationCodes.Status = Status.Inactive;
            authorizationCodes.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(authorizationCodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(authorizationCodes);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationCodeExists(authorizationCodes.Id))
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
        /// Unarchive a AuthorizationCodes by id
        /// </summary>
        /// <param name="id">AuthorizationCodes id</param>
        /// <returns></returns>
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(AuthorizationCode), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var authorizationCodes = await _context.AuthorizationCodes.FindAsync(id);

            if (authorizationCodes == null)
            {
                return NotFound();
            }

            authorizationCodes.Status = Status.Active;
            authorizationCodes.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(authorizationCodes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(authorizationCodes);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationCodeExists(authorizationCodes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool AuthorizationCodeExists(int id)
        {
            return _context.AuthorizationCodes.Any(e => e.Id == id);
        }
    }
}