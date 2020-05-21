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
    public class InventoryLiquidationsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public InventoryLiquidationsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/InventoryLiquidations
        [HttpGet]
        public IEnumerable<InventoryLiquidation> GetInventoryLiquidations()
        {
            return _context.InventoryLiquidations;
        }

        // GET: api/v1/Controllers/InventoryLiquidations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryLiquidation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventoryLiquidation = await _context.InventoryLiquidations.FindAsync(id);

            if (inventoryLiquidation == null)
            {
                return NotFound();
            }

            return Ok(inventoryLiquidation);
        }

        // PUT: api/v1/Controllers/InventoryLiquidations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryLiquidation([FromRoute] int id, [FromBody] InventoryLiquidation inventoryLiquidation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryLiquidation.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventoryLiquidation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryLiquidationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.InventoryLiquidations.Find(id));
        }

        // POST: api/v1/Controllers/InventoryLiquidations
        [HttpPost]
        public async Task<IActionResult> PostInventoryLiquidation([FromBody] InventoryLiquidation inventoryLiquidation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InventoryLiquidations.Add(inventoryLiquidation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventoryLiquidation", new { id = inventoryLiquidation.Id }, inventoryLiquidation);
        }

        // DELETE: api/v1/Controllers/InventoryLiquidations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryLiquidation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventoryLiquidation = await _context.InventoryLiquidations.FindAsync(id);
            if (inventoryLiquidation == null)
            {
                return NotFound();
            }

            _context.InventoryLiquidations.Remove(inventoryLiquidation);
            await _context.SaveChangesAsync();

            return Ok(inventoryLiquidation);
        }

        private bool InventoryLiquidationExists(int id)
        {
            return _context.InventoryLiquidations.Any(e => e.Id == id);
        }
    }
}