using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderTypesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public OrderTypesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/OrderTypes
        [HttpGet]
        public IEnumerable<OrderType> GetOrderTypes()
        {
            return _context.OrderTypes;
        }

        // GET: api/v1/Controllers/OrderTypes/Motives
        [HttpGet("motives")]
        public IEnumerable<OrderType> GetMotivesByOrderTypes()
        {
            // Get list of order type motives
            List<OrderType> orderTypes = _context.OrderTypes.ToList();

            // Verify whether order type motives is not empty
            if (orderTypes.Any())
            {
                // Iterate through all order types
                foreach (var orderType in orderTypes)
                {
                    // Get only motives that belongs to the proper order type
                    var motiveIds = _context.OrderTypeMotives
                        .Where(x => x.OrderTypeId == orderType.Id)
                        .Select(x => x.MotiveId)
                        .Distinct()
                        .ToList();

                    // Query motives by id
                    orderType.Motives = _context.Motives.Where(x => motiveIds.Contains(x.Id)).ToList();
                }

                return orderTypes;
            }

            // Build empty response
            return new List<OrderType>();
        }

        // GET: api/v1/Controllers/OrderTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderType = await _context.OrderTypes.FindAsync(id);

            if (orderType == null)
            {
                return NotFound();
            }

            return Ok(orderType);
        }

        // GET: api/v1/Controllers/OrderTypes/5/Motives
        [HttpGet("{id}/motives")]
        public async Task<IActionResult> GetMotivesByOrderType([FromRoute] int id)
        {
            // Get order type by id
            var orderType = await _context.OrderTypes.FindAsync(id);

            // Verify whether order type is not null
            if (orderType == null)
            {
                return NotFound();
            }

            // Get only motives that belongs to the proper order type
            var motiveIds = _context.OrderTypeMotives
                    .Where(x => x.OrderTypeId == orderType.Id)
                    .Select(x => x.MotiveId)
                    .Distinct()
                    .ToList();

            // Query motives by id
            orderType.Motives = _context.Motives.Where(x => motiveIds.Contains(x.Id)).ToList();

            return Ok(orderType);
        }

        // PUT: api/v1/Controllers/OrderTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderType([FromRoute] int id, [FromBody] OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderType.Id)
            {
                return BadRequest();
            }

            orderType.UpdatedAt = DateTimeOffset.Now;
            _context.Entry(orderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.OrderTypes.Find(id));
        }

        // POST: api/v1/Controllers/OrderTypes
        [HttpPost]
        public async Task<IActionResult> PostOrderType([FromBody] OrderType orderType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTimeOffset.Now;

            orderType.CreatedAt = now;
            orderType.UpdatedAt = now;

            orderType.Status = Status.Active;

            _context.OrderTypes.Add(orderType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderType", new { id = orderType.Id }, orderType);
        }

        // DELETE: api/v1/Controllers/OrderTypes/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderType = await _context.OrderTypes.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }

            _context.OrderTypes.Remove(orderType);
            await _context.SaveChangesAsync();

            return Ok(orderType);
        }

        /// <summary>
        /// Archive a OrderType by id
        /// </summary>
        /// <param name="id">OrderType id</param>
        /// <returns></returns>
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(OrderType), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var orderType = await _context.OrderTypes.FindAsync(id);

            if (orderType == null)
            {
                return NotFound();
            }

            orderType.Status = Status.Inactive;
            orderType.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(orderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(orderType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(orderType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Unarchive a OrderType by id
        /// </summary>
        /// <param name="id">OrderType id</param>
        /// <returns></returns>
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(OrderType), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var orderType = await _context.OrderTypes.FindAsync(id);

            if (orderType == null)
            {
                return NotFound();
            }

            orderType.Status = Status.Active;
            orderType.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(orderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(orderType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderTypeExists(orderType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool OrderTypeExists(int id)
        {
            return _context.OrderTypes.Any(e => e.Id == id);
        }
    }
}