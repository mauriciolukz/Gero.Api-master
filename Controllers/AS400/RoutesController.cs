using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models.AS400;
using System.Threading.Tasks;
using Gero.API.Helpers;
using Gero.API.Models;

namespace Gero.API.Controllers.AS400
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly BPCSContext _BPCSContext;
        private readonly DistributionContext _distributionContext;

        private readonly int DEFAULT_NUMBER_OF_MONTHS_BACK = 4;
        private readonly string NUMBER_OF_MONTHS_BACK_NAME = "NUMBER_OF_MONTHS_BACK";
        private readonly int DEFAULT_NUMBER_OF_PURCHASES = 5;
        private readonly string NUMBER_OF_PURCHASES_NAME = "NUMBER_OF_PURCHASES";

        public RoutesController(BPCSContext BPCSContext, DistributionContext distributionContext)
        {
            _BPCSContext = BPCSContext;
            _distributionContext = distributionContext;
        }

        /// <summary>
        /// Query all customers by a specific route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <param name="customerCode">Customer code to make an extra filter</param>
        /// <returns></returns>
        // GET: api/v1/Controllers/Routes/T01/Customers/99999999?
        [HttpGet("{routeCode}/customers/{customerCode?}")]
        public List<Models.AS400.Customer> GetCustomersByRoute(
            [FromRoute] string routeCode,
            [FromRoute] Decimal? customerCode = null
        )
        {
            // Create a new route helper instance
            Helpers.Route routeHelper = new Helpers.Route(_distributionContext);

            // Save route type
            var isDeliveryRoute = IsDeliveryRoute(routeCode);

            // Build query standard parameters
            List <QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.DATA_WAREHOUSE_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCodes}", value = $"'{routeCode}'" });

            // Query all customers
            List<Models.AS400.Customer> customers = _BPCSContext
                .Customers
                .FromSql(QueryBuilder.Build("GetCustomersByRoute.txt", parameters))
                .ToListAsync()
                .Result;

            // Verify whether customer code is not empty
            if (customerCode != null)
            {
                // Filter customer list by customer code
                customers = customers.FindAll(x => x.CustomerCode == customerCode);
            }

            // Verify whether route is for delivery or not
            if (isDeliveryRoute)
            {
                // Query orders and extract customer codes
                List<Models.Order> orders = routeHelper.GetRouteOrders(routeCode);

                // Verify whether any order was found
                if (orders.Count > 0)
                {
                    // Create an empty list to store the customer codes
                    List<int> customerCodes = new List<int>();

                    // Iterate through all orders found
                    foreach (var order in orders)
                    {
                        // Verify whether the customer codes list do not contain the same customer code
                        if (!customerCodes.Contains(Int32.Parse(order.CustomerCode)))
                        {
                            customerCodes.Add(Int32.Parse(order.CustomerCode));
                        }
                    }

                    // Verify whether the customer codes list is not empty
                    if (customerCodes.Count > 0)
                    {
                        // Filter customers list by customer codes
                        return customers
                            .FindAll(x => customerCodes.Contains(x.CustomerCode) || x.CreditLimit > 0);
                    }
                }
                else
                {
                    return new List<Models.AS400.Customer>();
                }
            }
            
            // Return  result
            return customers;
        }

        /// <summary>
        /// Query all prices list by a specific route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <param name="numberOfRecords">Number of records to be returned in the query result</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/price_list")]
        public List<PriceList> GetPriceListByRoute([FromRoute] string routeCode, [FromQuery] int numberOfRecords = 10)
        {
            // Create a new route helper instance
            Helpers.Route routeHelper = new Helpers.Route(_distributionContext);

            // Save route type
            var isDeliveryRoute = IsDeliveryRoute(routeCode);

            // Build query standard parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{numberOfRecords}", value = numberOfRecords.ToString() });
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.AS400_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            // Query all price lists
            var priceLists = _BPCSContext
                .PriceLists
                .FromSql(QueryBuilder.Build("GetPriceListByRoute.txt", parameters))
                .ToListAsync()
                .Result;

            // Verify whether route is for delivery or not
            if (isDeliveryRoute)
            {
                // Query orders and extract customer codes
                List<Models.Order> orders = routeHelper.GetRouteOrders(routeCode);

                // Verify whether any order was found
                if (orders.Count > 0)
                {
                    // Create an empty list to store the item codes
                    List<string> itemCodes = new List<string>();

                    // Iterate through all orders found
                    foreach (var order in orders)
                    {
                        // Get order items
                        List<OrderItem> items = order.Items;

                        // Verify whether any item was found in the order
                        if (items.Count > 0)
                        {
                            // Iterate through all items found
                            foreach (var item in items)
                            {
                                // Verify whether the item codes list do not contain the same item code
                                if (!itemCodes.Contains(item.ItemCode))
                                {
                                    itemCodes.Add(item.ItemCode);
                                }
                            }
                        }
                    }

                    // Verify whether the item codes list is not empty
                    if (itemCodes.Count > 0)
                    {
                        // Filter price lists by item codes
                        return priceLists.FindAll(x => itemCodes.Contains(x.ItemCode));
                    }
                }
                else
                {
                    return new List<PriceList>();
                }
            }

            return priceLists;
        }

        /// <summary>
        /// Query all receivables by a specific route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/receivables")]
        public Task<List<Receivable>> GetReceivablesByRoute([FromRoute] string routeCode)
        {
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.AS400_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            return _BPCSContext
                .Receivables
                .FromSql(QueryBuilder.Build("GetReceivablesByRoute.txt", parameters))
                .ToListAsync();
        }

        /// <summary>
        /// Query all items that will be the initial load for the route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/initial_load")]
        public List<InitialInventoryLoad> GetInitialLoadByRoute([FromRoute] string routeCode)
        {
            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.AS400_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            //// Create new initial inventory helper instance
            //Helpers.InitialInventory initialInventory = new Helpers.InitialInventory(_distributionContext);

            //// Verify if initial inventory has a record or not for today
            //if (!initialInventory.IsThereInventoryForToday(routeCode))
            //{
            //    // Get initial inventory with lot
            //    List<InitialInventoryLoadWithLot> initialInventoryLoadWithLots = _BPCSContext
            //        .InitialInventoryLoadWithLots
            //        .FromSql(QueryBuilder.Build("GetInitialInventoryLoadByRoute.txt", parameters))
            //        .ToListAsync()
            //        .Result;

            //    // Verify whether initial inventories with lots is not empty
            //    if (initialInventoryLoadWithLots.Count > 0)
            //    {
            //        // Create initial inventory for today
            //        initialInventory.LoadInventoryForToday(routeCode, initialInventoryLoadWithLots);
            //    }
            //}

            // Return initial inventories withouth lots
            return _BPCSContext
                .InitialInventoryLoads
                .FromSql(QueryBuilder.Build("GetInitialInventoryLoadByRoute.txt", parameters))
                .ToListAsync()
                .Result;
        }

        /// <summary>
        /// Query all items that will be the initial load for the route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/initial_load_with_lot")]
        public Task<List<InitialInventoryLoadWithLot>> GetInitialLoadWithLotByRoute([FromRoute] string routeCode)
        {
            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.AS400_PREFIX) });
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            return _BPCSContext
                .InitialInventoryLoadWithLots
                .FromSql(QueryBuilder.Build("GetInitialInventoryLoadWithLotByRoute.txt", parameters))
                .ToListAsync();
        }

        /// <summary>
        /// Query the purchase history by a specific route
        /// </summary>
        /// <param name="routeCode">Route code to make the filter (T01, P01)</param>
        /// <returns></returns>
        [HttpGet("{routeCode}/purchase_history")]
        public Task<List<PurchaseHistory>> GetPurchaseHistoryByRoute([FromRoute] string routeCode)
        {
            List<ApplicationSetting> applicationSettings = _distributionContext.ApplicationSettings.ToListAsync().Result;
            ApplicationSetting NUMBER_OF_MONTHS_BACK = applicationSettings.Find(x => x.Name == NUMBER_OF_MONTHS_BACK_NAME);
            ApplicationSetting NUMBER_OF_PURCHASES = applicationSettings.Find(x => x.Name == NUMBER_OF_PURCHASES_NAME);

            string numberOfMonthsBack = DEFAULT_NUMBER_OF_MONTHS_BACK.ToString();
            if (NUMBER_OF_MONTHS_BACK != null)
                numberOfMonthsBack = NUMBER_OF_MONTHS_BACK.Value;

            string numberOfPurchases = DEFAULT_NUMBER_OF_PURCHASES.ToString();
            if (NUMBER_OF_PURCHASES != null)
                numberOfPurchases = NUMBER_OF_PURCHASES.Value;

            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.DATA_WAREHOUSE_PREFIX) });
            parameters.Add(new QueryParameter { key = "{numberOfMonthsBack}", value = numberOfMonthsBack });
            parameters.Add(new QueryParameter { key = "{numberOfPurchases}", value = numberOfPurchases });
            parameters.Add(new QueryParameter { key = "{routeCode}", value = routeCode });

            return _BPCSContext
                .PurchaseHistories
                .FromSql(QueryBuilder.Build("GetPurchaseHistoryByRoute.txt", parameters))
                .ToListAsync();
        }

        private Boolean IsDeliveryRoute(string routeCode)
        {
            // Create a new route helper instance
            Helpers.Route routeHelper = new Helpers.Route(_distributionContext);

            // Save route type
            var isDeliveryRoute = true;

            // Get route detail
            Models.Route route = routeHelper.GetRoute(routeCode);

            // Verify whether the route found is empty or not
            if (route != null)
            {
                // Verify if route is for delivery or not
                isDeliveryRoute = route.RouteType.ToUpper() == Constants.DELIVERY;
            }

            return isDeliveryRoute;
        }
    }
}