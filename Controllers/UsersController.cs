using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Enumerations;
using System;
using System.Security.Principal;
using Gero.API.Helpers;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DistributionContext _context;

        public UsersController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all users
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/Users
        [HttpGet]
        public IEnumerable<Models.User> GetUsers()
        {
            return _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device);
        }

        /// <summary>
        /// Authenticate a user by username and password
        /// </summary>
        /// <param name="_user">Specify the user instance model</param>
        /// <example>
        ///     {
        ///         "username": "jperez",
        ///         "typedPassword": "12345"
        ///     }
        /// </example>
        /// <returns></returns>
        // POST: api/v1/controllers/Users/Authenticate
        [Route("authenticate")]
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Authenticate([FromHeader] string imei, [FromBody] Models.User _user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(imei))
                return Ok(new { error = "IMEI can not be empty" });

            var devices = await _context
                .Devices
                .Where(x => x.IMEI == imei)
                .ToListAsync();

            if (!devices.Any())
                return Ok(new { error = $"Device with IMEI {imei} does not exist" });

            var users = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == _user.Username)
                .ToListAsync();

            if (users.Any())
            {
                var user = users.First();

                var device = devices.First();

                var userDevices = await _context
                    .UserDevices
                    .Where(x => x.UserId == user.Id)
                    .Where(x => x.DeviceId == device.Id)
                    .ToListAsync();

                if (!userDevices.Any())
                    return Ok(new { error = $"Device {imei} is not assigned to user {user.Username}" });

                var userDevice = userDevices.First();

                if (userDevice.Status == Status.Inactive)
                    return Ok(new { error = $"Device {imei} for user {user.Username} is inactive" });

                if (user.Status == Status.Inactive)
                    return Ok(new { error = "User is invalid" });

                if (user.Status == Status.Created)
                {
                    if (user.ResetPasswordToken != _user.TypedPassword)
                        return Ok(new { error = "Token is invalid" });

                    if (DateTimeOffset.Now.CompareTo(user.ResetPasswordExpiredAt) == -1)
                    {
                        return Ok(user);
                    }
                    else
                    {
                        return Ok(new { error = "Token has expired" });
                    }
                }

                if (!Helpers.User.VerifyPassword(_user.TypedPassword, user.PasswordSalt, user.Password))
                    return Ok(new { error = "Password is invalid" });

                // Update user setting with last consecutive invoices
                UpdateUserSettingWithLastConsecutiveNumbers(user);

                // Update user roles with their synchronization steps
                UpdateUserRolesWithSynchronizationSteps(user);

                return Ok(user);
            } else
            {
                return Ok(new { error = "Username can not be found" });
            }
        }

        /// <summary>
        /// Query a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Users/jperez
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(Models.User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context
                .Users
                .Where(x => x.Username == username)
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .ToListAsync();

            if (users.Any())
            {
                var user = users.First();

                // Update user setting with last consecutive invoices
                UpdateUserSettingWithLastConsecutiveNumbers(user);

                // Update user roles with their synchronization steps
                UpdateUserRolesWithSynchronizationSteps(user);

                return Ok(user);
            } else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// User accepts the invitation after being created and type a new password for himself
        /// </summary>
        /// <param name="_user">Specify the user instance model</param>
        /// <example>
        ///     {
        ///         "username": "jperez",
        ///         "typedPassword": "12345"
        ///     }
        /// </example>
        /// <returns></returns>
        // POST: api/v1/Controllers/Users/Accept_Invitation
        [HttpPost("accept_invitation")]
        [ProducesResponseType(typeof(Models.User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(498)]
        public async Task<IActionResult> AcceptInvitation([FromBody] Models.User _user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == _user.Username)
                .ToListAsync();

            if (users.Any())
            {
                if (string.IsNullOrEmpty(_user.TypedPassword))
                    return StatusCode(403, "Password can not be empty");

                var user = users.First();

                Helpers.User.CreatePassword(_user.TypedPassword, out byte[] hashPasswordSalt, out byte[] hashPassword);

                user.PasswordSalt = hashPasswordSalt;
                user.Password = hashPassword;
                user.ResetPasswordToken = null;
                user.ResetPasswordSentAt = DateTimeOffset.MinValue;
                user.ResetPasswordExpiredAt = DateTimeOffset.MinValue;
                user.Status = Status.Active;
                user.UpdatedAt = DateTimeOffset.Now;

                // Update user setting with last consecutive invoices
                UpdateUserSettingWithLastConsecutiveNumbers(user);

                // Update user roles with their synchronization steps
                UpdateUserRolesWithSynchronizationSteps(user);

                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetUser", new { username = user.Username }, user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound("Username can not be found");
            }
        }

        /// <summary>
        /// Reset the user's password
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Reset_Password
        [HttpPatch("{username}/reset_password")]
        [ProducesResponseType(typeof(Models.User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ResetPassword([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == username)
                .FirstAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var now = DateTimeOffset.Now;

            user.Password = null;
            user.PasswordSalt = null;
            user.ResetPasswordToken = Helpers.RandomCode.Generate();
            user.ResetPasswordSentAt = now;
            user.ResetPasswordExpiredAt = now.AddDays(1);
            user.Status = Status.Created;
            user.UpdatedAt = now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        /// Update a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="user">Specify the user instance model</param>
        /// <returns></returns>
        // PUT: api/v1/Controllers/Users/jperez
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUser([FromRoute] string username, [FromBody] Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (_user == null)
                return NotFound("Username can not be found");

            // Keep same values from user
            user.Password = _user.Password;
            user.InvitationToken = _user.InvitationToken;
            user.InvitationSentAt = _user.InvitationSentAt;
            user.InvitationAcceptedAt = _user.InvitationAcceptedAt;
            user.ResetPasswordToken = _user.ResetPasswordToken;
            user.ResetPasswordSentAt = _user.ResetPasswordSentAt;
            user.ResetPasswordExpiredAt = _user.ResetPasswordExpiredAt;
            user.PasswordSalt = _user.PasswordSalt;
            user.Status = _user.Status;
            user.CreatedAt = _user.CreatedAt;
            user.UpdatedAt = DateTimeOffset.Now;

            // Detach user found in the query so the new user can be modified
            _context.Entry(_user).State = EntityState.Detached;
            
            // Add updated state to each user dependency
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(user.Setting).State = EntityState.Modified;
            _context.Entry(user.Info).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsernameExists(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            user = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == username)
                .FirstAsync();

            return Ok(user);
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">Specify the user instance model</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Users
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> PostUser([FromBody] Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (UsernameExists(user.Username))
                return StatusCode(403, "Username already exists");

            user.ResetPasswordToken = Helpers.RandomCode.Generate();
            user.Status = Status.Created;

            var now = DateTimeOffset.Now;
            
            user.ResetPasswordSentAt = now;
            user.ResetPasswordExpiredAt = now.AddDays(1);
            user.CreatedAt = now;
            user.UpdatedAt = now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        // DELETE: api/v1/Controllers/Users/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Archive a user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Archive
        [HttpPatch("{username}/archive")]
        [ProducesResponseType(typeof(Models.User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Archive([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == username)
                .FirstAsync();

            if (user == null)
                return NotFound("Username can not be found");

            user.Status = Status.Inactive;
            user.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        /// Unarchive a user by username and creates a new reset password token
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Unarchive
        [HttpPatch("{username}/unarchive")]
        [ProducesResponseType(typeof(Models.User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Unarchive([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context
                .Users
                .Include(x => x.Info)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Setting)
                .Include(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.Modules)
                            .ThenInclude(x => x.Module)
                                .ThenInclude(x => x.Children)
                .Include(x => x.Devices)
                    .ThenInclude(x => x.Device)
                .Where(x => x.Username == username)
                .FirstAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var now = DateTimeOffset.Now;

            user.Password = null;
            user.ResetPasswordToken = Helpers.RandomCode.Generate();
            user.ResetPasswordSentAt = now;
            user.ResetPasswordExpiredAt = now.AddDays(1);
            user.Status = Status.Created;
            user.UpdatedAt = now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        /// Query all devices assigned to the user
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Users/jperez/Devices
        [HttpGet("{username}/devices")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserDevices([FromRoute] string username)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var userDeviceIds = _context
                .UserDevices
                .Include(x => x.Device)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.DeviceId);

            var devices = new List<Device>();

            if (userDeviceIds.Any())
            {
                devices = _context.Devices.Where(x => userDeviceIds.Contains(x.Id)).ToList();
            }

            return StatusCode(200, new
            {
                username = user.Username,
                devices = devices
            });
        }

        /// <summary>
        /// Assign (Insert) a device to a corresponding user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="_userDevices">List of devices to be assigned to the user</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Users/jperez/Devices
        [HttpPost("{username}/devices")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostUserDevices([FromRoute] string username, [FromBody] List<UserDevice> _userDevices)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            if (_userDevices.Any())
            {
                var now = DateTimeOffset.Now;

                foreach (var userDevice in _userDevices)
                {
                    if (!_context.Devices.Any(x => x.Id == userDevice.DeviceId))
                        return NotFound($"Device {userDevice.DeviceId} does not exist");

                    if (_context.UserDevices.Any(x => x.UserId == user.Id && x.DeviceId == userDevice.DeviceId))
                        continue;

                    userDevice.UserId = user.Id;
                    userDevice.Status = Status.Active;
                    userDevice.CreatedAt = now;
                    userDevice.UpdatedAt = now;

                    _context.UserDevices.Add(userDevice);

                    await _context.SaveChangesAsync();
                }

                // Get devices ids
                List<int> deviceIds = _userDevices
                    .Select(x => x.DeviceId)
                    .ToList();

                // Verify whether any device id was found
                if (deviceIds.Any())
                {
                    // Select devices that do not come in the request and they have to be inactivated
                    List<UserDevice> missingUserDevices = _context
                        .UserDevices
                        .Where(x => x.UserId == user.Id)
                        .Where(x => !deviceIds.Contains(x.DeviceId))
                        .ToList();

                    // Verify whether any user device was found
                    if (missingUserDevices.Any())
                    {
                        // Iterate through all user devices found
                        foreach (var userDevice in missingUserDevices)
                        {
                            // Verify whether user device is already inactive
                            if (userDevice.Status == Status.Inactive)
                                continue;

                            // Update user device properties
                            userDevice.Status = Status.Inactive;
                            userDevice.UpdatedAt = DateTimeOffset.Now;

                            // Update user device with new properties
                            _context.Entry(userDevice).State = EntityState.Modified;

                            // Save changes to database
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                // Query all user devices
                List<UserDevice> userDevices = _context
                    .UserDevices
                    .Where(x => x.UserId == user.Id)
                    .ToList();

                return Ok(userDevices);
            } else
            {
                return BadRequest("Devices can not be empty");
            }
        }

        /// <summary>
        /// Archive a user-device instance
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="deviceId">Devices id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Devices/5/Archive
        [HttpPatch("{username}/devices/{deviceId}/archive")]
        public async Task<IActionResult> ArchiveUserDevice([FromRoute] string username, [FromRoute] int deviceId)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var userDevice = await _context
                .UserDevices
                .Where(x => x.UserId == user.Id)
                .Where(x => x.DeviceId == deviceId)
                .FirstOrDefaultAsync();

            if (userDevice == null)
                return NotFound(userDevice);

            if (userDevice.Status == Status.Inactive)
                return BadRequest($"User {user.Username} with device {deviceId} is already inactive");

            userDevice.Status = Status.Inactive;
            userDevice.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(userDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(userDevice);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// Uarchive a user-device instance
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="deviceId">Device id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Devices/5/Unarchive
        [HttpPatch("{username}/devices/{deviceId}/unarchive")]
        public async Task<IActionResult> UnarchiveUserDevice([FromRoute] string username, [FromRoute] int deviceId)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var userDevice = await _context
                .UserDevices
                .Where(x => x.UserId == user.Id)
                .Where(x => x.DeviceId == deviceId)
                .FirstOrDefaultAsync();

            if (userDevice == null)
                return NotFound(userDevice);

            if (userDevice.Status == Status.Active)
                return BadRequest($"User {user.Username} with device {deviceId} is already active");

            userDevice.Status = Status.Active;
            userDevice.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(userDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(userDevice);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// Assign (Insert) a role to a corresponding user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="userRoles">List of roles to be assigned to the user</param>
        /// <returns></returns>
        // POST: api/v1/Controllers/Users/jperez/Roles
        [HttpPost("{username}/roles")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PostUserRoles([FromRoute] string username, [FromBody] List<UserRole> userRoles)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            if (userRoles.Any())
            {
                var now = DateTimeOffset.Now;

                foreach (var userRole in userRoles)
                {
                    if (!_context.Roles.Any(x => x.Id == userRole.RoleId))
                        return NotFound($"Role {userRole.RoleId} does not exist");

                    if (_context.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == userRole.RoleId))
                        continue;

                    userRole.UserId = user.Id;
                    userRole.Status = Status.Active;
                    userRole.CreatedAt = now;
                    userRole.UpdatedAt = now;

                    _context.UserRoles.Add(userRole);

                    await _context.SaveChangesAsync();
                }

                // Get list of role ids
                List<int> roleIds = userRoles.Select(x => x.RoleId).ToList();

                // Verify whether any user role id was found
                if (roleIds.Any())
                {
                    List<UserRole> missingUserRoles = _context
                    .UserRoles
                    .Where(x => x.UserId == user.Id)
                    .Where(x => !roleIds.Contains(x.RoleId))
                    .Where(x => x.Status == Status.Active)
                    .ToList();

                    if (missingUserRoles.Any())
                    {
                        foreach(var userRole in missingUserRoles)
                        {
                            userRole.Status = Status.Inactive;
                            userRole.UpdatedAt = DateTimeOffset.Now;

                            _context.Entry(userRole).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                List<UserRole> _userRoles = _context
                    .UserRoles
                    .Where(x => x.UserId == user.Id)
                    .ToList();

                return Ok(_userRoles);
            }
            else
            {
                return BadRequest("Roles can not be empty");
            }
        }

        /// <summary>
        /// Archive a user-role instance
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="roleId">Role id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Roles/5/Archive
        [HttpPatch("{username}/roles/{roleId}/archive")]
        public async Task<IActionResult> ArchiveUserRole([FromRoute] string username, [FromRoute] int roleId)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var userRole = await _context
                .UserRoles
                .Where(x => x.UserId == user.Id)
                .Where(x => x.RoleId == roleId)
                .FirstOrDefaultAsync();

            if (userRole == null)
                return NotFound(userRole);

            if (userRole.Status == Status.Inactive)
                return BadRequest($"User {user.Username} with role {roleId} is already inactive");

            userRole.Status = Status.Inactive;
            userRole.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(userRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(userRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        /// <summary>
        /// Uarchive a user-role instance
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="roleId">Role id</param>
        /// <returns></returns>
        // PATCH: api/v1/Controllers/Users/jperez/Roles/5/Unarchive
        [HttpPatch("{username}/roles/{roleId}/unarchive")]
        public async Task<IActionResult> UnarchiveUserRole([FromRoute] string username, [FromRoute] int roleId)
        {
            var user = await _context
                .Users
                .Where(x => x.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Username can not be found");

            var userRole = await _context
                .UserRoles
                .Where(x => x.UserId == user.Id)
                .Where(x => x.RoleId == roleId)
                .FirstOrDefaultAsync();

            if (userRole == null)
                return NotFound(userRole);

            if (userRole.Status == Status.Active)
                return BadRequest($"User {user.Username} with role {roleId} is already active");

            userRole.Status = Status.Active;
            userRole.UpdatedAt = DateTimeOffset.Now;

            _context.Entry(userRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok(userRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private bool UsernameExists(string username)
        {
            return _context.Users.Any(e => e.Username == username);
        }

        private void UpdateUserSettingWithLastConsecutiveNumbers(Models.User user)
        {
            // Get user setting
            var setting = user.Setting;

            // Verify whether user setting is not null
            if (setting != null)
            {
                // Get route code from user setting
                string routeCode = setting.Route;

                // Verify emptiness of route code
                if (!string.IsNullOrEmpty(routeCode))
                {
                    // Create a new route helper instance
                    Helpers.Route routeHelper = new Helpers.Route(_context);

                    // Get route detail
                    Models.Route route = routeHelper.GetRoute(routeCode);

                    // Verify whether route is not from presale
                    if (route.RouteType.ToUpper() != Constants.PRESALE)
                    {
                        // Get consecutive invoices from route
                        List<ConsecutiveInvoice> consecutiveInvoices = _context
                            .ConsecutiveInvoices
                            .Where(x => x.Route == routeCode)
                            .ToList();

                        // Verify whether consecutive invoices list is not empty
                        if (consecutiveInvoices.Any())
                        {
                            // Get last invoice consecutive number
                            int lastInvoiceNumber = consecutiveInvoices
                                .Where(x => x.DocumentTypeId == "1")
                                .Select(x => x.SequenceNumber)
                                .First();

                            // Get last payment consecutive number
                            int lastPaymentNumber = consecutiveInvoices
                                .Where(x => x.DocumentTypeId == "2")
                                .Select(x => x.SequenceNumber)
                                .First();

                            // Assing values found to user setting
                            setting.LastInvoiceNumber = lastInvoiceNumber;
                            setting.LastPaymentNumber = lastPaymentNumber;
                        }
                    }
                }
            }
        }

        private void UpdateUserRolesWithSynchronizationSteps(Models.User user)
        {
            // Get roles by user
            List<UserRole> userRoles = user.Roles.ToList();

            // Verify whether user roles list is not empty
            if (userRoles.Any())
            {
                // Iterate through all roles by user
                foreach (var userRole in userRoles)
                {
                    // Get role from user role entity
                    Role role = userRole.Role;

                    // Verify whether role is not null
                    if (role != null)
                    {
                        // Find synchronization steps by role
                        List<int> synchronizationStepIds = _context
                            .SynchronizationStepByRoles
                            .Where(x => x.RoleId == role.Id)
                            .Where(x => x.Status == Status.Active)
                            .Select(x => x.SynchronizationStepId)
                            .ToList();

                        // Verify whether any synchronization step was found
                        if (synchronizationStepIds.Any())
                        {
                            // Find all synchronization steps by id
                            List<SynchronizationStep> synchronizationSteps = _context
                                .SynchronizationSteps
                                .Where(x => synchronizationStepIds.Contains(x.Id))
                                .ToList();

                            // Assign synchronization steps found to role
                            role.SynchronizationSteps = synchronizationSteps;
                        }
                        else
                        {
                            // Assign empty synchronization steps
                            role.SynchronizationSteps = new List<SynchronizationStep>();
                        }
                    }
                }
            }
        }
    }
}