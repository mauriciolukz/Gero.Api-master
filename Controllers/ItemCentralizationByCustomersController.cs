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
    public class ItemCentralizationByCustomersController : ControllerBase
    {
        private readonly DistributionContext _context;

        public  ItemCentralizationByCustomersController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ItemCentralizationByCustomers
        [HttpGet]
        public IEnumerable<ItemCentralizationByCustomer> GetItemCentralizationByCustomers()
        {
            return _context.ItemCentralizationByCustomers;
        }

        // GET: api/v1/Controllers/ItemCentralizationByCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemCentralizationByCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemCentralizationByCustomer = await _context.ItemCentralizationByCustomers.FindAsync(id);

            if (itemCentralizationByCustomer == null)
            {
                return NotFound();
            }

            return Ok(itemCentralizationByCustomer);
        }

        // PUT: api/v1/Controllers/ItemCentralizationByCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCentralizationByCustomer([FromRoute] int id, [FromBody] ItemCentralizationByCustomer itemCentralizationByCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemCentralizationByCustomer.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemCentralizationByCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCentralizationByCustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ItemCentralizationByCustomers.Find(id));
        }

        // POST: api/v1/Controllers/ItemCentralizationByCustomers
        [HttpPost]
        public async Task<IActionResult> PostItemCentralizationByCustomer([FromBody] ItemCentralizationByCustomer itemCentralizationByCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ItemCentralizationByCustomers.Add(itemCentralizationByCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCentralizationByCustomer", new { id = itemCentralizationByCustomer.Id }, itemCentralizationByCustomer);
        }

        // DELETE: api/v1/Controllers/ItemCentralizationByCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCentralizationByCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemCentralizationByCustomer = await _context.ItemCentralizationByCustomers.FindAsync(id);
            if (itemCentralizationByCustomer == null)
            {
                return NotFound();
            }

            _context.ItemCentralizationByCustomers.Remove(itemCentralizationByCustomer);
            await _context.SaveChangesAsync();

            return Ok(itemCentralizationByCustomer);
        }

        private bool ItemCentralizationByCustomerExists(int id)
        {
            return _context.ItemCentralizationByCustomers.Any(e => e.Id == id);
        }
    }
}