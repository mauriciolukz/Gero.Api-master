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

namespace Gero.API.Controllers.AS400
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly BPCSContext _BPCSContext;
        private readonly DistributionContext _distributionContext;

        public ItemsController(BPCSContext BPCSContext, DistributionContext distributionContext)
        {
            _BPCSContext = BPCSContext;
            _distributionContext = distributionContext;
        }

        // GET: api/v1/Controllers/Items
        [HttpGet]
        public IEnumerable<Item> GetItems([FromQuery] string search, [FromQuery] Boolean? isForExport)
        {
            // List all allowed items and get item code
            List<AllowedItem> allowedItems = _distributionContext.AllowedItems.ToList();

            // Verify whether the isForExport parameter is present
            if (isForExport != null)
            {
                // Filter allowed items by its flag
                allowedItems = allowedItems.Where(x => x.IsForExport == isForExport).ToList();
            }

            // Get allow item codes
            List<string> allowedItemCodes = allowedItems.Select(x => x.ItemCode).ToList();

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.AS400_PREFIX) });

            // List all items
            List<Item> items = _BPCSContext
                .Items
                .FromSql(QueryBuilder.Build("GetItems.txt", parameters))
                .ToList();

            // Verify whether any item exists
            if (items.Any())
            {
                // Verify if any allowed item was found
                if (allowedItems.Any())
                {
                    items = items
                        .Where(x => allowedItemCodes.Contains(x.ItemCode))
                        .ToList();
                }

                // Verify whether the search is not null or empty
                if (!string.IsNullOrEmpty(search))
                {
                    // Delete any inconsistency in search parameter
                    string updatedSearch = search.ToLower();

                    // Apply search filter
                    items = items
                        .Where(
                            x =>
                                x.ItemCode.ToLower().Contains(updatedSearch) ||
                                x.ItemName.ToLower().Contains(updatedSearch)
                        )
                        .ToList();
                }

                // Return result
                return items;
            }
            else
            {
                return new List<Item>();
            }
        }
    }
}