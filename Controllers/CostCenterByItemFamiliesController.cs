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
    public class CostCenterByItemFamiliesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public CostCenterByItemFamiliesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/CostCenterByItemFamilies
        [HttpGet]
        public IEnumerable<CostCenterByItemFamily> GetCostCenterByItemFamilies()
        {
            return _context.CostCenterByItemFamilies;
        }

        // GET: api/v1/Controllers/CostCenterByItemFamilies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCostCenterByItemFamily([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var costCenterByItemFamily = await _context.CostCenterByItemFamilies.FindAsync(id);

            if (costCenterByItemFamily == null)
            {
                return NotFound();
            }

            return Ok(costCenterByItemFamily);
        }

        // PUT: api/v1/Controllers/CostCenterByItemFamilies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCostCenterByItemFamily([FromRoute] int id, [FromBody] CostCenterByItemFamily costCenterByItemFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != costCenterByItemFamily.Id)
            {
                return BadRequest();
            }

            _context.Entry(costCenterByItemFamily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostCenterByItemFamilyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.CostCenterByItemFamilies.Find(id));
        }

        // POST: api/v1/Controllers/CostCenterByItemFamilies
        [HttpPost]
        public async Task<IActionResult> PostCostCenterByItemFamily([FromBody] CostCenterByItemFamily costCenterByItemFamily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CostCenterByItemFamilies.Add(costCenterByItemFamily);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCostCenterByItemFamily", new { id = costCenterByItemFamily.Id }, costCenterByItemFamily);
        }

        // DELETE: api/v1/Controllers/CostCenterByItemFamilies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCostCenterByItemFamily([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var costCenterByItemFamily = await _context.CostCenterByItemFamilies.FindAsync(id);
            if (costCenterByItemFamily == null)
            {
                return NotFound();
            }

            _context.CostCenterByItemFamilies.Remove(costCenterByItemFamily);
            await _context.SaveChangesAsync();

            return Ok(costCenterByItemFamily);
        }

        private bool CostCenterByItemFamilyExists(int id)
        {
            return _context.CostCenterByItemFamilies.Any(e => e.Id == id);
        }
    }
}