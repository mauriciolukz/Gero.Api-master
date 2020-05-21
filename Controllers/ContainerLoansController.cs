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
    public class ContainerLoansController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ContainerLoansController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ContainerLoans
        [HttpGet]
        public IEnumerable<ContainerLoan> GetContainerLoans()
        {
            return _context.ContainerLoans;
        }

        // GET: api/v1/Controllers/ContainerLoans/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContainerLoan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var containerLoan = await _context.ContainerLoans.FindAsync(id);

            if (containerLoan == null)
            {
                return NotFound();
            }

            return Ok(containerLoan);
        }

        // PUT: api/v1/Controllers/ContainerLoans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContainerLoan([FromRoute] int id, [FromBody] ContainerLoan containerLoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != containerLoan.Id)
            {
                return BadRequest();
            }

            _context.Entry(containerLoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerLoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ContainerLoans.Find(id));
        }

        // POST: api/v1/Controllers/ContainerLoans
        [HttpPost]
        public async Task<IActionResult> PostContainerLoan([FromBody] ContainerLoan containerLoan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ContainerLoans.Add(containerLoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContainerLoan", new { id = containerLoan.Id }, containerLoan);
        }

        // DELETE: api/v1/Controllers/ContainerLoans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContainerLoan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var containerLoan = await _context.ContainerLoans.FindAsync(id);
            if (containerLoan == null)
            {
                return NotFound();
            }

            _context.ContainerLoans.Remove(containerLoan);
            await _context.SaveChangesAsync();

            return Ok(containerLoan);
        }

        private bool ContainerLoanExists(int id)
        {
            return _context.ContainerLoans.Any(e => e.Id == id);
        }
    }
}