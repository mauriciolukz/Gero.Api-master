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
    public class CountriesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public CountriesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all countries
        /// </summary>
        /// <param name="withDependencies">Specify whether iterates through the country relationships or not</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Countries
        [HttpGet]
        public IEnumerable<Country> GetCountries([FromHeader] bool withDependencies = false)
        {
            if (withDependencies)
            {
                return _context
                    .Countries
                    .Include(x => x.Departments)
                        .ThenInclude(x => x.Municipalities)
                    .ToList();
            }
            else
            {
                return _context.Countries;
            }
        }

        /// <summary>
        /// Query a country by id
        /// </summary>
        /// <param name="id">Country id</param>
        /// <param name="withDependencies">Specify whether iterates through the country relationships or not</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Countries/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Country), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountry([FromRoute] int id, [FromHeader] bool withDependencies = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = null;

            if (withDependencies)
            {
                country = await _context
                    .Countries
                    .Include(x => x.Departments)
                        .ThenInclude(x => x.Municipalities)
                    .Where(x => x.Id == id)
                    .FirstAsync();
            }
            else
            {
                country = await _context.Countries.FindAsync(id);
            }

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        /// <summary>
        /// Update a country by id
        /// </summary>
        /// <param name="id">Country id</param>
        /// <param name="country">Specify the country instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Countries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry([FromRoute] int id, [FromBody] Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Countries.Find(id));
        }

        /// <summary>
        /// Create a country
        /// </summary>
        /// <param name="country">Specify the country instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Countries
        [HttpPost]
        public async Task<IActionResult> PostCountry([FromBody] Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/v1/Controllers/Countries/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return Ok(country);
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}