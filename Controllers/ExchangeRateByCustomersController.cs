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
    public class ExchangeRateByCustomersController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ExchangeRateByCustomersController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ExchangeRateByCustomers
        [HttpGet]
        public IEnumerable<ExchangeRateByCustomer> GetExchangeRateByCustomers()
        {
            return _context.ExchangeRateByCustomers;
        }

        // GET: api/v1/Controllers/ExchangeRateByCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExchangeRateByCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exchangeRateByCustomer = await _context.ExchangeRateByCustomers.FindAsync(id);

            if (exchangeRateByCustomer == null)
            {
                return NotFound();
            }

            return Ok(exchangeRateByCustomer);
        }

        // PUT: api/v1/Controllers/ExchangeRateByCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExchangeRateByCustomer([FromRoute] int id, [FromBody] ExchangeRateByCustomer exchangeRateByCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exchangeRateByCustomer.Id)
            {
                return BadRequest();
            }

            _context.Entry(exchangeRateByCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRateByCustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ExchangeRateByCustomers.Find(id));
        }

        // POST: api/v1/Controllers/ExchangeRateByCustomers
        [HttpPost]
        public async Task<IActionResult> PostExchangeRateByCustomer([FromBody] ExchangeRateByCustomer exchangeRateByCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ExchangeRateByCustomers.Add(exchangeRateByCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRateByCustomer", new { id = exchangeRateByCustomer.Id }, exchangeRateByCustomer);
        }

        // DELETE: api/v1/Controllers/ExchangeRateByCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExchangeRateByCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exchangeRateByCustomer = await _context.ExchangeRateByCustomers.FindAsync(id);
            if (exchangeRateByCustomer == null)
            {
                return NotFound();
            }

            _context.ExchangeRateByCustomers.Remove(exchangeRateByCustomer);
            await _context.SaveChangesAsync();

            return Ok(exchangeRateByCustomer);
        }

        private bool ExchangeRateByCustomerExists(int id)
        {
            return _context.ExchangeRateByCustomers.Any(e => e.Id == id);
        }
    }
}