using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models.AS400;
using Gero.API.Helpers;
using Gero.API.Models;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MotivesNotToSellTypesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public MotivesNotToSellTypesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all motives not to sells
        /// </summary>
        /// <returns></returns>
        // GET: api/v1/Controllers/MotivesNotToSellTypes
        [HttpGet]
        public IEnumerable<MotivesNotToSellType> GetMotivesNotToSellTypes()
        {
            return _context
                .MotivesNotToSellTypes
                .FromSql(QueryBuilder.Build("GetMotivesNotToSellTypes.txt", null))
                .ToList();
        }
    }
}