using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using System;
using Gero.API.Enumerations;
using Microsoft.AspNetCore.Authorization;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public DevicesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all devices
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Devices
        [HttpGet]
        public IEnumerable<Device> GetDevice()
        {
            return _context.Devices;
        }

        /// <summary>
        /// Query a device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns></returns>
        // GET: api/v1/Devices/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        /// <summary>
        /// Update a device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <param name="device">Specify the device instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Devices/5
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.Id)
            {
                return BadRequest();
            }

            var _device = await _context.Devices.FindAsync(id);

            device.Status = _device.Status;
            device.CreatedAt = _device.CreatedAt;
            device.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(_device).State = EntityState.Detached;

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Devices.Find(id));
        }

        /// <summary>
        /// Create a device
        /// </summary>
        /// <param name="device">Specify the device instance model</param>
        /// <returns></returns>
        // POST: api/v1/Devices
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostDevice([FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            device.Status = Status.Active;

            var now = DateTimeOffset.Now;

            device.CreatedAt = now;
            device.UpdatedAt = now;

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        // DELETE: api/v1/Devices/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        /// <summary>
        /// Archive a device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Devices/5/Archive
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            device.Status = Status.Inactive;
            device.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(device);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.Id))
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
        /// Unarchive a device by id
        /// </summary>
        /// <param name="id">Device id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Devices/5/Unarchive
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var device = await _context.Devices.FindAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            device.Status = Status.Active;
            device.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(device);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}