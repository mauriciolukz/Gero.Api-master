using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Helpers;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponUsersController : ControllerBase
    {
        private readonly DistributionContext _context;

        public CouponUsersController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/CouponUsers
        [HttpGet]
        public IEnumerable<CouponUser> GetCouponUsers()
        {
            return _context.CouponUsers;
        }

        // GET: api/v1/Controllers/CouponUsers/{ApproverTypeCode}
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{ApproverTypeCode}/approvers")]
        public async Task<IActionResult> GetCouponUsersByApproverTypeCode([FromRoute] string approverTypeCode)
        {
            if (string.IsNullOrEmpty(approverTypeCode))
            {
                return BadRequest("Approver type code can not be empty");
            }

            // Find a match with approver type model
            var approverType = await _context
                .ApproverTypes
                .Where(x => x.Code == approverTypeCode)
                .FirstOrDefaultAsync();

            if (approverType == null)
            {
                return NotFound("Approver type code does not exist");
            }

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{approverTypeCode}", value = approverTypeCode });

            // Query coupon users by approver type model
            var couponUsersByApproverType = _context
                .CouponUserByApproverTypes
                .FromSql(QueryBuilder.Build("GetCouponUsersByApproverTypeCode.txt", parameters))
                .ToList();

            // Verify whether any result was found
            if (couponUsersByApproverType.Any())
            {
                // Iterate by each result
                foreach (var couponUserByApproverType in couponUsersByApproverType)
                {
                    // Verify whether next approver type code is null
                    if (approverType.NextCode == null)
                    {
                        // Assign empty list
                        couponUserByApproverType.Approvers = new List<CouponUserByApproverType>();
                    }
                    else
                    {
                        // List approvers
                        List<CouponUserWithApprover> couponUserWithApprovers = await _context
                            .CouponUserWithApprovers
                            .Where(x => x.ApplicantUserId == couponUserByApproverType.Id)
                            .Where(x => x.Status == Enumerations.Status.Active)
                            .ToListAsync();

                        // Verify whether approvers are empty or not
                        if (couponUserWithApprovers.Any())
                        {
                            // Remove everything from parameters
                            parameters.Clear();

                            // Get approver ids
                            List<int> approverIds = couponUserWithApprovers.Select(x => x.ApproverUserId).ToList();

                            // Update parameters
                            parameters.Add(new QueryParameter { key = "{approverTypeCode}", value = approverType.NextCode });
                            parameters.Add(new QueryParameter { key = "{couponUserIds}", value = $"'{string.Join("','", approverIds)}'" });

                            // Assign proper approvers with its dependencies
                            couponUserByApproverType.Approvers = _context
                                .CouponUserByApproverTypes
                                .FromSql(QueryBuilder.Build("GetCouponUsersByIdsAndApproverTypeCode.txt", parameters))
                                .ToList();
                        }
                        else
                        {
                            // Assign empty list
                            couponUserByApproverType.Approvers = new List<CouponUserByApproverType>();
                        }
                    }
                }

                return Ok(couponUsersByApproverType);
            }
            else
            {
                return NotFound("None user was found with the approver type code given");
            }
        }

        // GET: api/v1/Controllers/CouponUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couponUser = await _context
                .CouponUsers
                .Include(x => x.ApproverType)
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            if (couponUser == null)
            {
                return NotFound();
            }

            // Get approver type
            ApproverType approverType = couponUser.ApproverType;

            // Get coupon user with approvers
            List<CouponUserWithApprover> couponUserWithApprovers = await _context
                .CouponUserWithApprovers
                .Where(x => x.ApplicantUserId == couponUser.Id)
                .Where(x => x.Status == Enumerations.Status.Active)
                .ToListAsync();

            // Verify wheteher coupon user with approvers was not found
            if (!couponUserWithApprovers.Any())
            {
                return NotFound();
            }

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{approverTypeCode}", value = approverType.Code });
            parameters.Add(new QueryParameter { key = "{couponUserIds}", value = $"'{couponUserWithApprovers.FirstOrDefault().ApplicantUserId}'" });

            // Load coupon user by approver type
            CouponUserByApproverType couponUserByApproverType = _context
                .CouponUserByApproverTypes
                .FromSql(QueryBuilder.Build("GetCouponUsersByIdsAndApproverTypeCode.txt", parameters))
                .ToList()
                .FirstOrDefault();

            // Verify whether coupon user by approver type is not null
            if (couponUserByApproverType == null)
            {
                return NotFound();
            }

            // Get approver ids
            List<int> approverIds = couponUserWithApprovers
                .Select(x => x.ApproverUserId)
                .ToList();

            // Update parameters
            parameters.First().value = approverType.NextCode;
            parameters.Last().value = $"'{string.Join("','", approverIds)}'";

            // Assign proper approvers with its dependencies
            couponUserByApproverType.Approvers = _context
                .CouponUserByApproverTypes
                .FromSql(QueryBuilder.Build("GetCouponUsersByIdsAndApproverTypeCode.txt", parameters))
                .ToList();

            return Ok(couponUserByApproverType);
        }

        // GET: api/v1/Controllers/CouponUsers/5
        [HttpGet("{id}/{ApproverTypeCode}")]
        public async Task<IActionResult> GetCouponUser([FromRoute] int id, [FromRoute] string approverTypeCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couponUser = await _context
                .CouponUsers
                .Include(x => x.ApproverType)
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();

            if (couponUser == null)
            {
                return NotFound();
            }

            // Get approver type
            ApproverType approverType = couponUser.ApproverType;

            // Get coupon user with approvers
            List<CouponUserWithApprover> couponUserWithApprovers = await _context
                .CouponUserWithApprovers
                .Where(x => x.ApplicantUserId == couponUser.Id)
                .Where(x => x.Status == Enumerations.Status.Active)
                .ToListAsync();

            // Verify wheteher coupon user with approvers was not found
            if (!couponUserWithApprovers.Any())
            {
                return NotFound();
            }

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{approverTypeCode}", value = approverType.Code });
            parameters.Add(new QueryParameter { key = "{couponUserIds}", value = $"'{couponUserWithApprovers.FirstOrDefault().ApplicantUserId}'" });

            // Load coupon user by approver type
            CouponUserByApproverType couponUserByApproverType = _context
                .CouponUserByApproverTypes
                .FromSql(QueryBuilder.Build("GetCouponUsersByIdsAndApproverTypeCode.txt", parameters))
                .ToList()
                .FirstOrDefault();

            // Verify whether coupon user by approver type is not null
            if (couponUserByApproverType == null)
            {
                return NotFound();
            }

            // Get approver ids
            List<int> approverIds = couponUserWithApprovers
                .Select(x => x.ApproverUserId)
                .ToList();

            // Update parameters
            parameters.First().value = approverTypeCode;
            parameters.Last().value = $"'{string.Join("','", approverIds)}'";

            // Assign proper approvers with its dependencies
            couponUserByApproverType.Approvers = _context
                .CouponUserByApproverTypes
                .FromSql(QueryBuilder.Build("GetCouponUsersByIdsAndApproverTypeCode.txt", parameters))
                .ToList();

            return Ok(couponUserByApproverType);
        }

        // PUT: api/v1/Controllers/CouponUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponUser([FromRoute] int id, [FromBody] CouponUser couponUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != couponUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(couponUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.CouponUsers.Find(id));
        }

        // POST: api/v1/Controllers/CouponUsers
        [HttpPost]
        public async Task<IActionResult> PostCouponUser([FromBody] CouponUser couponUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CouponUsers.Add(couponUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCouponUser", new { id = couponUser.Id }, couponUser);
        }

        // DELETE: api/v1/Controllers/CouponUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var couponUser = await _context.CouponUsers.FindAsync(id);
            if (couponUser == null)
            {
                return NotFound();
            }

            _context.CouponUsers.Remove(couponUser);
            await _context.SaveChangesAsync();

            return Ok(couponUser);
        }

        private bool CouponUserExists(int id)
        {
            return _context.CouponUsers.Any(e => e.Id == id);
        }
    }
}