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
    public class DepartmentsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public DepartmentsController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all departments
        /// </summary>
        /// <param name="withDependencies">Specify whether iterates through the department relationships or not</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Departments
        [HttpGet]
        public IEnumerable<Department> GetDepartments([FromHeader] bool withDependencies = false)
        {
            if (withDependencies)
            {
                return _context
                    .Departments
                    .Include(x => x.Municipalities)
                    .ToList();
            }
            else
            {
                return _context.Departments;
            }
        }

        /// <summary>
        /// Query a department by id
        /// </summary>
        /// <param name="id">Department id</param>
        /// <param name="withDependencies">Specify whether iterates through the department relationships or not</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Departments/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Department), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetDepartment([FromRoute] int id, [FromHeader] bool withDependencies = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Department department = null;

            if (withDependencies)
            {
                department = await _context
                    .Departments
                    .Include(x => x.Municipalities)
                    .Where(x => x.Id == id)
                    .FirstAsync();
            }
            else
            {
                department = await _context.Departments.FindAsync(id);
            }

            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        /// <summary>
        /// Update a department by id
        /// </summary>
        /// <param name="id">Department id</param>
        /// <param name="department">Specify the department instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment([FromRoute] int id, [FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Departments.Find(id));
        }

        /// <summary>
        /// Create a department
        /// </summary>
        /// <param name="department">Specify the department instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Departments
        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        // DELETE: api/v1/Controllers/Departments/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return Ok(department);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}