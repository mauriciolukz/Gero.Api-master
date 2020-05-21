using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models.AS400;
using Gero.API.Helpers;
using Gero.API.Models;

namespace Gero.API.Controllers.AS400
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BPCSContext _BPCSContext;
        private readonly DistributionContext _distributionContext;

        public CustomersController(BPCSContext BPCSContext, DistributionContext distributionContext)
        {
            _BPCSContext = BPCSContext;
            _distributionContext = distributionContext;
        }

        // GET: api/v1/Controllers/Customers
        [HttpGet]
        public IEnumerable<Models.AS400.Customer> GetCustomers([FromQuery] string search)
        {
            List<string> routeCodes = new List<string>();
            routeCodes.Add("A01");
            routeCodes.Add("A02");
            routeCodes.Add("A03");
            routeCodes.Add("A04");
            routeCodes.Add("A05");
            routeCodes.Add("A06");
            routeCodes.Add("A07");
            routeCodes.Add("A08");
            routeCodes.Add("A09");
            routeCodes.Add("A10");
            routeCodes.Add("A11");
            routeCodes.Add("A12");
            routeCodes.Add("A13");
            routeCodes.Add("A14");
            routeCodes.Add("A15");
            routeCodes.Add("A16");
            routeCodes.Add("A17");
            routeCodes.Add("A18");
            routeCodes.Add("A19");
            routeCodes.Add("A20");
            routeCodes.Add("A21");
            routeCodes.Add("B01");
            routeCodes.Add("BCH");
            routeCodes.Add("BFC");
            routeCodes.Add("BPF");
            routeCodes.Add("BTA");
            routeCodes.Add("C42");
            routeCodes.Add("T01");
            routeCodes.Add("T02");
            routeCodes.Add("T03");
            routeCodes.Add("T04");
            routeCodes.Add("T05");
            routeCodes.Add("T06");
            routeCodes.Add("T07");
            routeCodes.Add("T08");
            routeCodes.Add("T09");
            routeCodes.Add("T10");
            routeCodes.Add("T11");
            routeCodes.Add("T12");
            routeCodes.Add("T13");
            routeCodes.Add("T14");
            routeCodes.Add("T15");
            routeCodes.Add("T16");
            routeCodes.Add("T17");
            routeCodes.Add("T18");
            routeCodes.Add("T19");
            routeCodes.Add("T20");
            routeCodes.Add("T21");
            routeCodes.Add("T22");
            routeCodes.Add("T23");
            routeCodes.Add("T24");
            routeCodes.Add("T25");
            routeCodes.Add("T26");
            routeCodes.Add("T27");
            routeCodes.Add("T28");
            routeCodes.Add("T29");
            routeCodes.Add("T30");
            routeCodes.Add("T31");
            routeCodes.Add("T32");
            routeCodes.Add("T33");
            routeCodes.Add("T34");
            routeCodes.Add("T35");
            routeCodes.Add("T36");
            routeCodes.Add("T37");
            routeCodes.Add("T38");
            routeCodes.Add("T39");
            routeCodes.Add("T40");
            routeCodes.Add("T41");
            routeCodes.Add("T42");
            routeCodes.Add("T43");
            routeCodes.Add("T44");
            routeCodes.Add("T45");
            routeCodes.Add("T46");
            routeCodes.Add("T47");
            routeCodes.Add("T50");
            routeCodes.Add("T51");
            routeCodes.Add("T52");
            routeCodes.Add("T53");
            routeCodes.Add("T54");
            routeCodes.Add("T55");
            routeCodes.Add("T56");
            routeCodes.Add("T57");
            routeCodes.Add("T58");
            routeCodes.Add("T59");
            routeCodes.Add("T60");
            routeCodes.Add("T61");
            routeCodes.Add("T62");
            routeCodes.Add("T63");
            routeCodes.Add("T64");
            routeCodes.Add("V01");
            routeCodes.Add("V02");

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.DATA_WAREHOUSE_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCodes}", value = $"'{string.Join("','", routeCodes)}'" });

            // List all customers by routes
            List<Models.AS400.Customer> customers = _BPCSContext
                .Customers
                .FromSql(QueryBuilder.Build("GetCustomersByRoute.txt", parameters))
                .ToList();

            // Verify whether the search is not null or empty
            if (!string.IsNullOrEmpty(search))
            {
                // Delete any inconsistency in search parameter
                string updatedSearch = search.ToLower();

                // Apply search filter
                customers = customers
                    .Where(
                        x =>
                            x.CustomerCode.ToString().Contains(updatedSearch) ||
                            x.CustomerName.ToLower().Contains(updatedSearch) ||
                            x.BusinessName.ToLower().Contains(updatedSearch)
                    )
                    .ToList();
            }

            // Return result
            return customers;

            //// Get current domain user
            //string username = User.Identity.Name;

            //// Verify whether the user authenticated exists
            //if (string.IsNullOrEmpty(username))
            //{
            //    // Return empty result
            //    return new List<Customer>();
            //}
            //else
            //{
            //    // Build query parameters
            //    List<QueryParameter> parameters = new List<QueryParameter>();
            //    parameters.Add(new QueryParameter { key = "{username}", value = username.Replace(@"\\", @"\") });

            //    // Get list of routes associated to current logged user
            //    List<Models.Route> routes = _distributionContext
            //        .Routes
            //        .FromSql(QueryBuilder.Build("GetRoutesFromDomainUser.txt", parameters))
            //        .ToList();

            //    // Verify whether any route was found
            //    if (routes.Any())
            //    {
            //        // Get only the route code from the list
            //        List<string> routeCodes = routes.Select(x => x.RouteCode).ToList();

            //        // Remove previous parameter
            //        parameters.RemoveAt(0);

            //        // Build extra parameters
            //        parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.DATA_WAREHOUSE_PREFIX) });
            //        parameters.Add(new QueryParameter { key = "{routeCodes}", value = $"'{string.Join("','", routeCodes)}'" });

            //        // List all customers by routes
            //        List<Customer> customers = _BPCSContext
            //            .Customers
            //            .FromSql(QueryBuilder.Build("GetCustomersByRoute.txt", parameters))
            //            .ToList();

            //        // Verify whether the search is not null or empty
            //        if (!string.IsNullOrEmpty(search))
            //        {
            //            // Delete any inconsistency in search parameter
            //            string updatedSearch = search.ToLower();

            //            // Apply search filter
            //            customers = customers
            //                .Where(
            //                    x =>
            //                        x.CustomerCode.ToString().Contains(updatedSearch) ||
            //                        x.CustomerName.ToLower().Contains(updatedSearch) ||
            //                        x.BusinessName.ToLower().Contains(updatedSearch)
            //                )
            //                .ToList();
            //        }

            //        // Return result
            //        return customers;
            //    }
            //    else
            //    {
            //        // Return empty result
            //        return new List<Customer>();
            //    }
            //}
        }
    }
}