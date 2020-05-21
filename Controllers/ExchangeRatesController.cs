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
    public class ExchangeRatesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ExchangeRatesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ExchangeRates
        [HttpGet]
        public IEnumerable<ExchangeRate> GetExchangeRates()
        {
            return _context.ExchangeRates;
        }

        // GET: api/v1/Controllers/ExchangeRates/current
        [HttpGet("current")]
        public async Task<IActionResult> GetExchangeRateCurrent()
        {
            var exchangeRate = await _context
                .ExchangeRates
                .Where(
                    x =>
                        x.ExchangeDate.Year == DateTime.Now.Year &&
                        x.ExchangeDate.Month == DateTime.Now.Month &&
                        x.ExchangeDate.Day == DateTime.Now.Day
                )
                .FirstOrDefaultAsync();

            if (exchangeRate == null)
            {
                exchangeRate = await _context
                    .ExchangeRates
                    .LastOrDefaultAsync();
            }

            return Ok(exchangeRate);
        }

        // GET: api/v1/Controllers/ExchangeRates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExchangeRate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exchangeRate = await _context.ExchangeRates.FindAsync(id);

            if (exchangeRate == null)
            {
                return NotFound();
            }

            return Ok(exchangeRate);
        }

        // PUT: api/v1/Controllers/ExchangeRates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExchangeRate([FromRoute] int id, [FromBody] ExchangeRate exchangeRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exchangeRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(exchangeRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ExchangeRates.Find(id));
        }

        // POST: api/v1/Controllers/ExchangeRates
        [HttpPost]
        public async Task<IActionResult> PostExchangeRate([FromBody] ExchangeRate exchangeRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ExchangeRates.Add(exchangeRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRate", new { id = exchangeRate.Id }, exchangeRate);
        }

        // DELETE: api/v1/Controllers/ExchangeRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExchangeRate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exchangeRate = await _context.ExchangeRates.FindAsync(id);
            if (exchangeRate == null)
            {
                return NotFound();
            }

            _context.ExchangeRates.Remove(exchangeRate);
            await _context.SaveChangesAsync();

            return Ok(exchangeRate);
        }

        private bool ExchangeRateExists(int id)
        {
            return _context.ExchangeRates.Any(e => e.Id == id);
        }
    }
}