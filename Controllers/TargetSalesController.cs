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
    public class TargetSalesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public TargetSalesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/TargetSales
        [HttpGet]
        public IEnumerable<TargetSale> GetTargetSales()
        {
            return _context.TargetSales;
        }

        // GET: api/v1/Controllers/TargetSales/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTargetSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var targetSale = await _context.TargetSales.FindAsync(id);

            if (targetSale == null)
            {
                return NotFound();
            }

            return Ok(targetSale);
        }

        // PUT: api/v1/Controllers/TargetSales/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTargetSale([FromRoute] int id, [FromBody] TargetSale targetSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != targetSale.Id)
            {
                return BadRequest();
            }

            _context.Entry(targetSale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetSaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.TargetSales.Find(id));
        }

        // POST: api/v1/Controllers/TargetSales
        [HttpPost]
        public async Task<IActionResult> PostTargetSale([FromBody] TargetSale targetSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TargetSales.Add(targetSale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTargetSale", new { id = targetSale.Id }, targetSale);
        }

        // DELETE: api/v1/Controllers/TargetSales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTargetSale([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var targetSale = await _context.TargetSales.FindAsync(id);
            if (targetSale == null)
            {
                return NotFound();
            }

            _context.TargetSales.Remove(targetSale);
            await _context.SaveChangesAsync();

            return Ok(targetSale);
        }

        private bool TargetSaleExists(int id)
        {
            return _context.TargetSales.Any(e => e.Id == id);
        }
    }
}