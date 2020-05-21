using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorizationCodeTypesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public AuthorizationCodeTypesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/AuthorizationCodeTypes
        [HttpGet]
        public IEnumerable<AuthorizationCodeType> GetAuthorizationCodeTypes()
        {
            return _context.AuthorizationCodeTypes;
        }

        // GET: api/v1/Controllers/AuthorizationCodeTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorizationCodeType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorizationCodeType = await _context.AuthorizationCodeTypes.FindAsync(id);

            if (authorizationCodeType == null)
            {
                return NotFound();
            }

            return Ok(authorizationCodeType);
        }

        // PUT: api/v1/Controllers/AuthorizationCodeTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorizationCodeType([FromRoute] int id, [FromBody] AuthorizationCodeType authorizationCodeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authorizationCodeType.Id)
            {
                return BadRequest();
            }

            _context.Entry(authorizationCodeType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationCodeTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.AuthorizationCodeTypes.Find(id));
        }

        // POST: api/v1/Controllers/AuthorizationCodeTypes
        [HttpPost]
        public async Task<IActionResult> PostAuthorizationCodeType([FromBody] AuthorizationCodeType authorizationCodeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AuthorizationCodeTypes.Add(authorizationCodeType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorizationCodeType", new { id = authorizationCodeType.Id }, authorizationCodeType);
        }

        // DELETE: api/v1/Controllers/AuthorizationCodeTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthorizationCodeType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authorizationCodeType = await _context.AuthorizationCodeTypes.FindAsync(id);
            if (authorizationCodeType == null)
            {
                return NotFound();
            }

            _context.AuthorizationCodeTypes.Remove(authorizationCodeType);
            await _context.SaveChangesAsync();

            return Ok(authorizationCodeType);
        }

        private bool AuthorizationCodeTypeExists(int id)
        {
            return _context.AuthorizationCodeTypes.Any(e => e.Id == id);
        }
    }
}