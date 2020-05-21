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
    public class ItemImagesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public ItemImagesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/ItemImages
        [HttpGet]
        public IEnumerable<ItemImage> GetItemImages()
        {
            return _context.ItemImages;
        }

        // GET: api/v1/Controllers/ItemImages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemImage = await _context.ItemImages.FindAsync(id);

            if (itemImage == null)
            {
                return NotFound();
            }

            return Ok(itemImage);
        }

        // PUT: api/v1/Controllers/ItemImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemImage([FromRoute] int id, [FromBody] ItemImage itemImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemImage.Id)
            {
                return BadRequest();
            }

            itemImage.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(itemImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.ItemImages.Find(id));
        }

        // POST: api/v1/Controllers/ItemImages
        [HttpPost]
        public async Task<IActionResult> PostItemImage([FromBody] ItemImage itemImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            itemImage.Status = Status.Active;

            var now = DateTimeOffset.Now;

            itemImage.CreatedAt = now;
            itemImage.UpdatedAt = now;

            _context.ItemImages.Add(itemImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemImage", new { id = itemImage.Id }, itemImage);
        }

        // DELETE: api/v1/Controllers/ItemImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var itemImage = await _context.ItemImages.FindAsync(id);
            if (itemImage == null)
            {
                return NotFound();
            }

            _context.ItemImages.Remove(itemImage);
            await _context.SaveChangesAsync();

            return Ok(itemImage);
        }

        private bool ItemImageExists(int id)
        {
            return _context.ItemImages.Any(e => e.Id == id);
        }
    }
}