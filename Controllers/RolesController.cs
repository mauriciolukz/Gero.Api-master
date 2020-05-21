using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public RolesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all roles
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/Roles
        [HttpGet]
        public IEnumerable<Role> GetRoles([FromQuery] RoleType? roleType)
        {
            if (roleType == null)
                return _context
                    .Roles
                    .Include(x => x.Modules)
                        .ThenInclude(x => x.Module)
                            .ThenInclude(x => x.Children);
            else
                return _context
                    .Roles
                    .Where(x => x.RoleType == roleType)
                    .Include(x => x.Modules)
                        .ThenInclude(x => x.Module)
                            .ThenInclude(x => x.Children);
        }

        /// <summary>
        /// Query a role by id
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Roles/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Role), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context
                .Roles
                .Include(x => x.Modules)
                    .ThenInclude(x => x.Module)
                        .ThenInclude(x => x.Children)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        /// <summary>
        /// Update a role by id
        /// </summary>
        /// <param name="id">role id</param>
        /// <param name="role">Specify the role instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole([FromRoute] int id, [FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != role.Id)
            {
                return BadRequest();
            }

            var _role = await _context.Roles.FindAsync(id);

            role.Status = _role.Status;
            role.CreatedAt = _role.CreatedAt;
            role.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(_role).State = EntityState.Detached;

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Roles.Find(id));
        }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role">Specify the role instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Roles
        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            role.Status = Status.Active;

            var now = DateTimeOffset.Now;

            role.CreatedAt = now;
            role.UpdatedAt = now;

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // DELETE: api/v1/Controllers/Roles/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        /// <summary>
        /// Query all modules assigned to the role
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Users/{jperez}/devices
        [HttpGet("{id}/modules")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRoleModules([FromRoute] int id)
        {
            var role = await _context
                .Roles
                .Include(x => x.Modules)
                    .ThenInclude(x => x.Module)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
                return NotFound("Role can not be found");

            return Ok(role);
        }

        /// <summary>
        /// Assign (Insert) a module to a corresponding role
        /// </summary>
        /// <param name="id">Role id</param>
        /// <param name="_roleModules">List of modules to be assigned to the role</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Roles/5/Modules
        [HttpPost("{id}/modules")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostRoleModules([FromRoute] int id, [FromBody] List<RoleModule> _roleModules)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return NotFound("Role can not be found");

            if (_roleModules.Any())
            {
                var now = DateTimeOffset.Now;

                foreach (var roleModule in _roleModules)
                {
                    roleModule.RoleId = role.Id;
                    roleModule.Status = Status.Active;
                    roleModule.CreatedAt = now;
                    roleModule.UpdatedAt = now;

                    _context.RoleModules.Add(roleModule);
                    await _context.SaveChangesAsync();
                }

                return Ok(role.Modules);
            }
            else
            {
                return BadRequest("Modules can not be empty");
            }
        }

        /// <summary>
        /// Archive a role-module instance
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="moduleId">Module id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Roles/5/Modules/5/Archive
        [HttpPatch("{roleId}/modules/{moduleId}/archive")]
        public async Task<IActionResult> ArchiveRoleModule([FromRoute] int roleId, [FromRoute] int moduleId)
        {
            var roleModule = await _context
                .RoleModules
                .Where(x => x.RoleId == roleId)
                .Where(x => x.ModuleId == moduleId)
                .FirstOrDefaultAsync();

            if (roleModule == null)
                return NotFound(roleModule);

            if (roleModule.Status == Status.Inactive)
                return BadRequest($"Role {roleId} with module {moduleId} is already inactive");

            roleModule.Status = Status.Inactive;
            roleModule.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(roleModule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(roleModule);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// Uarchive a role-module instance
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <param name="moduleId">Module id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Roles/5/Modules/5/Unarchive
        [HttpPatch("{roleId}/modules/{moduleId}/unarchive")]
        public async Task<IActionResult> UnarchiveRoleModule([FromRoute] int roleId, [FromRoute] int moduleId)
        {
            var roleModule = await _context
                .RoleModules
                .Where(x => x.RoleId == roleId)
                .Where(x => x.ModuleId == moduleId)
                .FirstOrDefaultAsync();

            if (roleModule == null)
                return NotFound(roleModule);

            if (roleModule.Status == Status.Active)
                return BadRequest($"Role {roleId} with module {moduleId} is already active");

            roleModule.Status = Status.Active;
            roleModule.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(roleModule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(roleModule);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        // POST: api/v1/Controllers/Roles/5/Modules
        [HttpPost("{id}/synchronizationsteps")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostSynchronizationStepsByRole([FromRoute] int id, [FromBody] List<SynchronizationStepByRole> _synchronizationSteps)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return NotFound("Role can not be found");

            if (_synchronizationSteps.Any())
            {
                foreach (var synchronizationStep in _synchronizationSteps)
                {
                    synchronizationStep.RoleId = role.Id;
                    synchronizationStep.Status = Status.Active;

                    _context.SynchronizationStepByRoles.Add(synchronizationStep);
                    await _context.SaveChangesAsync();
                }

                return Ok(role.SynchronizationSteps);
            }
            else
            {
                return BadRequest("Modules can not be empty");
            }
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}