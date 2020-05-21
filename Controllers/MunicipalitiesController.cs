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
    public class MunicipalitiesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public MunicipalitiesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/Municipalities
        [HttpGet]
        public IEnumerable<Municipality> GetMunicipalities()
        {
            return _context.Municipalities;
        }

        // GET: api/v1/Controllers/Municipalities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMunicipality([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var municipality = await _context.Municipalities.FindAsync(id);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(municipality);
        }

        // PUT: api/v1/Controllers/Municipalities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMunicipality([FromRoute] int id, [FromBody] Municipality municipality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != municipality.Id)
            {
                return BadRequest();
            }

            _context.Entry(municipality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MunicipalityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Municipalities.Find(id));
        }

        // POST: api/v1/Controllers/Municipalities
        [HttpPost]
        public async Task<IActionResult> PostMunicipality([FromBody] Municipality municipality)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMunicipality", new { id = municipality.Id }, municipality);
        }

        // DELETE: api/v1/Controllers/Municipalities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMunicipality([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var municipality = await _context.Municipalities.FindAsync(id);
            if (municipality == null)
            {
                return NotFound();
            }

            _context.Municipalities.Remove(municipality);
            await _context.SaveChangesAsync();

            return Ok(municipality);
        }

        private bool MunicipalityExists(int id)
        {
            return _context.Municipalities.Any(e => e.Id == id);
        }
    }
}