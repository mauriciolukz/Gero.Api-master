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
    public class ApproverTypesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ApproverTypesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ApproverTypes
        [HttpGet]
        public IEnumerable<ApproverType> GetApproverTypes()
        {
            return _context.ApproverTypes;
        }

        // GET: api/v1/Controllers/ApproverTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApproverType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var approverType = await _context.ApproverTypes.FindAsync(id);

            if (approverType == null)
            {
                return NotFound();
            }

            return Ok(approverType);
        }

        // PUT: api/v1/Controllers/ApproverTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApproverType([FromRoute] int id, [FromBody] ApproverType approverType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != approverType.Id)
            {
                return BadRequest();
            }

            _context.Entry(approverType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApproverTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ApproverTypes.Find(id));
        }

        // POST: api/v1/Controllers/ApproverTypes
        [HttpPost]
        public async Task<IActionResult> PostApproverType([FromBody] ApproverType approverType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApproverTypes.Add(approverType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApproverType", new { id = approverType.Id }, approverType);
        }

        // DELETE: api/v1/Controllers/ApproverTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApproverType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var approverType = await _context.ApproverTypes.FindAsync(id);
            if (approverType == null)
            {
                return NotFound();
            }

            _context.ApproverTypes.Remove(approverType);
            await _context.SaveChangesAsync();

            return Ok(approverType);
        }

        private bool ApproverTypeExists(int id)
        {
            return _context.ApproverTypes.Any(e => e.Id == id);
        }
    }
}