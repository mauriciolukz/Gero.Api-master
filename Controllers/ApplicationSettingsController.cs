using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gero.API.Enumerations;
using Gero.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApplicationSettingsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ApplicationSettingsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/ApplicationSettings
        [HttpGet]
        public IEnumerable<ApplicationSetting> Get()
        {
            return _context.ApplicationSettings;
        }

        // GET: api/v1/ApplicationSettings/applicationSettings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationSetting([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationSetting = await _context.ApplicationSettings.FindAsync(id);

            if (applicationSetting == null)
            {
                return NotFound();
            }

            return Ok(applicationSetting);
        }

        // POST: api/v1/ApplicationSettings
        [HttpPost]
        public async Task<IActionResult> PostApplicationSetting([FromBody] ApplicationSetting applicationSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var now = DateTimeOffset.Now;

            applicationSetting.Status = Status.Active;

            applicationSetting.CreatedAt = now;
            applicationSetting.UpdatedAt = now;

            _context.ApplicationSettings.Add(applicationSetting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationSetting", new { id = applicationSetting.Id }, applicationSetting);
        }

        // PUT: api/v1/ApplicationSettings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationSetting([FromRoute] int id, [FromBody] ApplicationSetting applicationSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationSetting.Id)
            {
                return BadRequest();
            }

            applicationSetting.UpdatedAt = DateTimeOffset.Now;
            _context.Entry(applicationSetting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationSettingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ApplicationSettings.Find(id));
        }

        // DELETE: api/v1/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationSetting([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationSettings = await _context.ApplicationSettings.FindAsync(id);
            if (applicationSettings == null)
            {
                return NotFound();
            }

            _context.ApplicationSettings.Remove(applicationSettings);
            await _context.SaveChangesAsync();

            return Ok(applicationSettings);
        }

        /// <summary>
        /// Archive a ApplicationSetting by id
        /// </summary>
        /// <param name="id">ApplicationSetting id</param>
        /// <returns></returns>
        [HttpPatch("{id}/archive")]
        [ProducesResponseType(typeof(ApplicationSetting), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] int id)
        {
            var applicationSettings = await _context.ApplicationSettings.FindAsync(id);

            if (applicationSettings == null)
            {
                return NotFound();
            }

            applicationSettings.Status = Status.Inactive;
            applicationSettings.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(applicationSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(applicationSettings);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationSettingExists(applicationSettings.Id))
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
        /// Unarchive a ApplicationSetting by id
        /// </summary>
        /// <param name="id">ApplicationSettings id</param>
        /// <returns></returns>
        [HttpPatch("{id}/unarchive")]
        [ProducesResponseType(typeof(ApplicationSetting), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] int id)
        {
            var applicationSettings = await _context.ApplicationSettings.FindAsync(id);

            if (applicationSettings == null)
            {
                return NotFound();
            }

            applicationSettings.Status = Status.Active;
            applicationSettings.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(applicationSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(applicationSettings);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationSettingExists(applicationSettings.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ApplicationSettingExists(int id)
        {
            return _context.OrderTypes.Any(e => e.Id == id);
        }
    }
}
