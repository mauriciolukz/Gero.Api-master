using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using Gero.API.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public RoutesController(DistributionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Query all routes
        /// </summary>
        /// <returns></returns>
        // GET: api/Controllers/Routes
        [HttpGet]
        public IEnumerable<Models.Route> GetRoutes([FromQuery] Boolean userFromDomain = false)
        {
            if (userFromDomain)
            {
                // Get current domain user
                string username = User.Identity.Name;

                // Verify whether the user authenticated exists
                if (string.IsNullOrEmpty(username))
                {
                    // Return empty result
                    return new List<Models.Route>();
                }

                // Build query parameters
                List<QueryParameter> parameters = new List<QueryParameter>();
                parameters.Add(new QueryParameter { key = "{username}", value = username.Replace(@"\\", @"\") });

                // Get list of routes associated to current logged user
                return _context
                    .Routes
                    .FromSql(QueryBuilder.Build("GetRoutesFromDomainUser.txt", parameters))
                    .ToList();
            }
            else
            {
                return _context
                    .Routes
                    .FromSql(QueryBuilder.Build("GetRoutes.txt", null))
                    .ToList();
            }
        }

        [HttpGet("{routeCode}")]
        public Models.Route GetRoute([FromRoute] string routeCode)
        {
            // Build new route helper
            Helpers.Route routeHelper = new Helpers.Route(_context);

            // Return result
            return routeHelper.GetRoute(routeCode);
        }

        /// <summary>
        /// Query all orders that will be delivered by the route
        /// </summary>
        /// <param name="routeCode">Delivery route</param>
        /// <returns></returns>
        // GET: api/Controllers/Routes
        [HttpGet("{routeCode}/delivery_orders")]
        public IEnumerable<Models.Order> GetDeliveryOrders([FromRoute] string routeCode)
        {
            // Build new route helper
            Helpers.Route routeHelper = new Helpers.Route(_context);

            // Get orders list to be delivered today
            List<Models.Order> orders = routeHelper.GetRouteOrders(routeCode);

            // Verify whether orders list is not empty
            if (orders.Any())
            {
                // Iterate through all orders found
                foreach (var order in orders)
                {
                    // Find container return by order number
                    var containerReturn = _context
                        .ContainerReturns
                        .Include(x => x.Items)
                        .Where(x => x.ReturnNumber == order.OrderNumber)
                        .FirstOrDefault();

                    // Verify whether container return is not null
                    if (containerReturn != null)
                    {
                        // Assign container return found
                        order.Container = containerReturn;
                    }
                }
            }

            // Return result
            return orders;
        }

        [HttpGet("{routeCode}/banks_with_payment_types")]
        public Task<List<BankWithPaymentType>> GetBanksWithPaymentTypes([FromRoute] string routeCode)
        {
            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            // Execute query
            return _context
                .BankWithPaymentTypes
                .FromSql(QueryBuilder.Build("GetBanksWithPaymentTypesByRoute.txt", parameters))
                .ToListAsync();
        }

        /// <summary>
        /// Query the advance target sales from a specific route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/AdvanceTargetSales")]
        public Task<List<AdvanceTargetSale>> GetAdvanceTargetSalesByRoute([FromRoute] string routeCode)
        {
            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();          
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            return _context
                .AdvanceTargetSales
                .FromSql(QueryBuilder.Build("GetAdvanceTargetSaleByRoute.txt", parameters))
                .ToListAsync();
        }

        [HttpGet("{routeCode}/authorization_codes")]
        public List<AuthorizationCode> GetAuthorizationCodes([FromRoute] string routeCode)
        {
            // Verify whether route is not presale
            if (IsPresaleRoute(routeCode))
            {
                // Return empty list
                return new List<AuthorizationCode>();
            }

            // Query authorization codes from database
            List<AuthorizationCode> authorizationCodes = _context
                .AuthorizationCodes
                .Where(x => x.Route == routeCode)
                .Where(
                    x =>
                        x.CreatedAt.Year == DateTime.Now.Year &&
                        x.CreatedAt.Month == DateTime.Now.Month &&
                        x.CreatedAt.Day == DateTime.Now.Day
                )
                .ToList();

            // Verify whether none authorization code is already save in the database
            if (!authorizationCodes.Any())
            {
                // Create authorization codes quantity key
                string AUTHORIZATION_CODES_QUANTITY = "AUTHORIZATION_CODES_QUANTITY";

                // Create authorization codes quantity
                int authorizationCodesQuantity = 30;

                // Get quantity of authorization codes to be created from application settings
                ApplicationSetting applicationSetting = _context
                    .ApplicationSettings
                    .Where(x => x.Name == AUTHORIZATION_CODES_QUANTITY)
                    .FirstOrDefault();

                // Verify whether any application setting was found in the database
                if (applicationSetting != null)
                {
                    authorizationCodesQuantity = Int32.Parse(applicationSetting.Value);
                }

                // Create password options
                PasswordOptions passwordOptions = new PasswordOptions()
                {
                    RequiredLength = 8,
                    RequiredUniqueChars = 4,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = false,
                    RequireUppercase = false
                };

                // Iterate through the number of authorization codes to be created
                for (int i = 0; i < authorizationCodesQuantity; i++)
                {
                    // Create a brand new random code by route
                    string randomCode = CreateRandomCode(routeCode, passwordOptions);

                    // Create a new authorization code entity
                    AuthorizationCode authorizationCode = new AuthorizationCode
                    {
                        Code = randomCode,
                        Route = routeCode,
                        Status = Enumerations.Status.Active,
                        CreatedAt = DateTimeOffset.Now
                    };

                    // Add authorization code to database context
                    _context.AuthorizationCodes.Add(authorizationCode);

                    // Save authorization code
                    _context.SaveChanges();

                    // Add new authorization code created to authorization code list
                    authorizationCodes.Add(authorizationCode);
                }
            }

            return authorizationCodes;
        }

        private Boolean IsPresaleRoute(string routeCode)
        {
            // Create a new route helper instance
            Helpers.Route routeHelper = new Helpers.Route(_context);

            // Save route type
            var isPresaleRoute = false;

            // Get route detail
            Models.Route route = routeHelper.GetRoute(routeCode);

            // Verify whether the route found is empty or not
            if (route != null)
            {
                // Verify if route is for delivery or not
                isPresaleRoute = route.RouteType.ToUpper() == Constants.PRESALE;
            }

            return isPresaleRoute;
        }

        private string CreateRandomCode(string routeCode, PasswordOptions passwordOptions)
        {
            // Create random code
            string randomCode = RandomCode.Generate(passwordOptions);

            // Verify if random codes already exists with this route
            if (_context.AuthorizationCodes.Any(x => x.Route == routeCode && x.Code == randomCode))
            {
                // Generate a brand new random code
                return CreateRandomCode(routeCode, passwordOptions);
            }

            return randomCode;
        }
    }
}