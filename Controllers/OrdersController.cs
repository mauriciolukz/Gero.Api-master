using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gero.API.Models;
using System.Globalization;
using Gero.API.Helpers;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DistributionContext _context;

        public OrdersController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/Orders
        [HttpGet]
        public IEnumerable<Models.Order> GetOrders()
        {
            return _context
                .Orders
                .Include(x => x.Items)
                .Include(x => x.OrderType)
                .Take(2);
        }

        [HttpGet("dispatch")]
        public async Task<IActionResult> GetDispatch([FromQuery] string routeCode, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            // FIXME: Validate emptiness query string values

            // Format string dates
            DateTime formattedStartDate = DateTimeOffset.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime formattedEndDate = DateTimeOffset.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            // List orders that apply to the filters
            List<Models.Order> orders = await _context
                .Orders
                .Include(x => x.Items)
                .Where(x => x.DeliveryRoute == routeCode)
                .Where(x => x.DeliveryDate.Date >= formattedStartDate && x.DeliveryDate.Date <= formattedEndDate)
                .Where(x => x.IsOrderConfirmed == true)
                .Where(x => x.IsOrderFinished == false)
                .Where(x => x.ConsolidatedOrderId == null)
                .ToListAsync();

            // Verify whether there is any order found
            if (orders.Any())
            {
                // Create empty list
                List<Dispatch> result = new List<Dispatch>();

                // Iterate through all orders found
                foreach (var order in orders)
                {
                    // Get items from each order
                    var items = order.Items;

                    // Iterate through all items found in the order
                    foreach (var item in items)
                    {
                        // Try to find a item from the current list
                        var itemFromResult = result
                            .FirstOrDefault(
                                x =>
                                    x.ItemCode == item.ItemCode &&
                                    x.OrderTypeId == order.OrderTypeId &&
                                    x.DeliveryDate.Date == order.DeliveryDate.Date &&
                                    x.RouteCode == order.DeliveryRoute
                            );

                        // If not result found in the list, let's create a new dispatch object
                        if (itemFromResult == null)
                        {
                            // Add new dispatch item to the result
                            result.Add(new Dispatch
                            {
                                ItemCode = item.ItemCode,
                                Quantity = item.TotalOfUnits,
                                OrderTypeId = order.OrderTypeId,
                                DeliveryDate = order.DeliveryDate,
                                RouteCode = order.DeliveryRoute
                            });
                        }
                        else
                        {
                            var previousQuantity = itemFromResult.Quantity;
                            var currentQuantity = previousQuantity + item.TotalOfUnits;

                            // Update the quantity with the summatory of the previous quantity with the current quantity
                            itemFromResult.Quantity = currentQuantity;
                        }
                    }
                }

                // Get order numbers list
                List<string> orderNumbers = orders.Select(x => x.OrderNumber).ToList();

                // Return result in proper JSON
                return Ok(new { Items = result, OrderNumbers = orderNumbers });
            }
            else
            {
                return NotFound(new List<Dispatch>());
            }
        }

        [HttpGet("dispatch/pending_routes")]
        public List<PendingRoutesToDispatch> GetPendingRoutesToDispatch([FromQuery] string warehouseCode, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            // Format string dates
            string formattedStartDate = startDate.Replace("-", "");
            string formattedEndDate = endDate.Replace("-", "");

            // Build query parameters
            List<QueryParameter> parameters = new List<QueryParameter>();
            parameters.Add(new QueryParameter { key = "{warehouseCode}", value = $"{warehouseCode}" });
            parameters.Add(new QueryParameter { key = "{startDate}", value = $"{formattedStartDate}" });
            parameters.Add(new QueryParameter { key = "{endDate}", value = $"{formattedEndDate}" });

            return _context
                .PendingRoutesToDispatch
                .FromSql(QueryBuilder.Build("GetPendingRoutesToDispatch.txt", parameters))
                .ToList();
        }

        [HttpGet("dispatch_by_order_number")]
        public async Task<IActionResult> GetDispatchByOrderNumber([FromQuery] string routeCode, [FromQuery] string startDate, [FromQuery] string endDate)
        {
            // FIXME: Validate emptiness query string values

            // Format string dates
            DateTime formattedStartDate = DateTimeOffset.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;
            DateTime formattedEndDate = DateTimeOffset.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date;

            // List orders that apply to the filters
            List<Models.Order> orders = await _context
                .Orders
                .Include(x => x.Items)
                .Where(x => x.DeliveryRoute == routeCode)
                .Where(x => x.DeliveryDate.Date >= formattedStartDate && x.DeliveryDate.Date <= formattedEndDate)
                .Where(x => x.IsOrderConfirmed == true)
                .Where(x => x.IsOrderFinished == false)
                .Where(x => x.ConsolidatedOrderId == null)
                .ToListAsync();

            // Verify whether there is any order found
            if (orders.Any())
            {
                // Get customer codes list
                List<string> customerCodes = orders
                    .Select(x => x.CustomerCode)
                    .ToList();

                // Create empty list of customers
                List<Customer> customers = new List<Customer>();

                // Verify whether customer codes is not empty
                if (customerCodes.Any())
                {
                    // Build query parameters
                    List<QueryParameter> parameters = new List<QueryParameter>();
                    parameters.Add(new QueryParameter { key = "{schema}", value = AS400Schema.GetSchema(Constants.DATA_WAREHOUSE_PREFIX) });
                    parameters.Add(new QueryParameter { key = "{customerCodes}", value = $"'{string.Join("','", customerCodes)}'" });

                    customers = _context
                        .Customers
                        .FromSql(QueryBuilder.Build("GetCustomersById.txt", parameters))
                        .ToList();
                }

                // Create empty list
                List<Object> result = new List<Object>();

                // Iterate through all orders found
                foreach (var order in orders)
                {
                    // Create empty list for result items
                    List<Object> resultItems = new List<Object>();

                    // Get items from each order
                    var items = order.Items;

                    // Iterate through all items found in the order
                    foreach (var item in items)
                    {
                        // Add new dispatch item to the result item
                        resultItems.Add(new
                        {
                            item.ItemCode,
                            Quantity = item.TotalOfUnits                          
                        });
                    }

                    // Add result item to result
                    result.Add(new
                    {
                        order.OrderNumber,
                        order.CustomerCode,
                        customerName = customers.Any()
                            ?
                                customers
                                    .Where(x => x.CustomerCode == order.CustomerCode)
                                    .Select(x => x.CustomerName).FirstOrDefault()
                            :
                                "",
                        order.OrderTypeId,
                        order.DeliveryDate,
                        RouteCode = order.DeliveryRoute,
                        Items = resultItems
                    });
                }

                // Return result in proper JSON
                return Ok(result);
            }
            else
            {
                return NotFound(new List<Object>());
            }
        }

        [HttpPost("dispatch/{pickingId}")]
        public async Task<IActionResult> PostDispatch([FromRoute] int pickingId, [FromBody] List<string> orderNumbers)
        {
            if (orderNumbers.Any())
            {
                // Get list of orders by order numbers
                List<Models.Order> orders = await _context
                    .Orders
                    .Where(x => orderNumbers.Contains(x.OrderNumber))
                    .Where(x => x.ConsolidatedOrderId == null)
                    .ToListAsync();

                // Verify whether any order was found by order number
                if (orders.Any())
                {
                    // Get current date/time
                    DateTimeOffset now = DateTimeOffset.Now;

                    // Create new empty consolidated order object
                    ConsolidatedOrder consolidatedOrder = new ConsolidatedOrder();
                    consolidatedOrder.RouteCode = orders.First().DeliveryRoute;
                    consolidatedOrder.PickingId = pickingId;
                    consolidatedOrder.OrderStatus = 0; // Assigned
                    consolidatedOrder.CreatedAt = now;
                    consolidatedOrder.UpdatedAt = now;

                    // Add consolidated order to database context
                    _context.ConsolidatedOrders.Add(consolidatedOrder);

                    // Save new consolidated order
                    await _context.SaveChangesAsync();

                    // Iterate through all orders found
                    foreach (var order in orders)
                    {
                        // Assign consolidated order id to order
                        order.ConsolidatedOrderId = consolidatedOrder.Id;

                        // Add modify status to entity
                        _context.Entry(order).State = EntityState.Modified;

                        // Save changes
                        await _context.SaveChangesAsync();
                    }

                    // Return new consolidated order created
                    return Ok(consolidatedOrder);
                }
                else
                {
                    return NotFound(new { error = "Non order found into the list of order number given" });
                }
            }
            else
            {
                return BadRequest(new { error = "Order numbers can not be empty" });
            }
        }

        // GET: api/v1/Controllers/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context
                .Orders
                .Include(x => x.Items)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            // Find containers by order number
            var containerReturn = await _context
                .ContainerReturns
                .Include(x => x.Items)
                .Where(x => x.ReturnNumber == order.OrderNumber)
                .FirstOrDefaultAsync();

            // Verify whether container return is not null
            if (containerReturn != null)
            {
                // Assign container return found
                order.Container = containerReturn;
            }

            return Ok(order);
        }

        // PUT: api/v1/Controllers/Orders
        [HttpPut]
        public async Task<IActionResult> PutOrder([FromBody] List<Models.Order> orders)
        {
            // Verify whether any order was sent to the server
            if (orders.Any())
            {
                // Create new empty list to save orders that were already processed to the server
                List<Object> updatedOrders = new List<Object>();

                // Create new empty list to save orders that could not be processed to the server
                List<Object> errorOrders = new List<Object>();

                // Iterate through all orders sent from web/mobile application
                foreach (var order in orders)
                {
                    // Get order items
                    List<OrderItem> items = order.Items;

                    // Verify whether the order has items
                    if (items == null || !items.Any())
                    {
                        // Add order to error list if not any item was specified
                        errorOrders.Add(new { order, error = "Items can not be empty" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get order items ids
                    List<int> itemIds = items.Where(x => x.Id != null).Select(x => x.Id).ToList();

                    // Verify if any item has id
                    if (itemIds.Any())
                    {
                        // Get orders items ids that do not exist in the current list so those will be deleted
                        List<OrderItem> deletedItems = _context
                            .OrderItems
                            .Where(x => x.OrderId == order.Id)
                            .Where(x => !itemIds.Contains(x.Id))
                            .ToList();

                        // Iterate through all deleted items
                        foreach (var deletedItem in deletedItems)
                        {
                            // Add deleted state to item
                            _context.Entry(deletedItem).State = EntityState.Deleted;
                        }
                    }

                    // Iterate through all items in the order
                    foreach (var item in items)
                    {
                        // Verify whether the item is already created or not
                        if (item.Id == null || item.Id == 0)
                        {
                            // Assign order id to the item
                            item.OrderId = order.Id;

                            // Add new item
                            _context.Entry(item).State = EntityState.Added;
                        }
                        else
                        {
                            // Update item
                            _context.Entry(item).State = EntityState.Modified;
                        }
                    }

                    // Get order container
                    ContainerReturn container = order.Container;

                    // Verify whether container is null or not
                    if (container == null)
                    {
                        // Get container return from order number
                        ContainerReturn containerReturn = await _context
                            .ContainerReturns
                            .Include(x => x.Items)
                            .Where(x => x.ReturnNumber == order.OrderNumber)
                            .FirstOrDefaultAsync();

                        // Verify wheteher container return is not null
                        if (containerReturn != null)
                        {
                            // Get container return items
                            List<ContainerReturnItem> containerReturnItems = containerReturn.Items;

                            // Iterate through all container return items
                            foreach (var containerReturnItem in containerReturnItems)
                            {
                                // Delete container return item
                                _context.Entry(containerReturnItem).State = EntityState.Deleted;
                            }

                            // Delete container return
                            _context.Entry(containerReturn).State = EntityState.Deleted;
                        }
                    }
                    else
                    {
                        // Get container return items
                        List<ContainerReturnItem> containerReturnItems = container.Items;

                        // Iterate through all container return items
                        foreach (var containerReturnItem in containerReturnItems)
                        {
                            // Verify whether the container return item is already created or not
                            if (containerReturnItem.Id == null || containerReturnItem.Id == 0)
                            {
                                // Assign container return id to the container return item
                                containerReturnItem.ContainerReturnId = container.Id;

                                // Add new container return item
                                _context.Entry(containerReturnItem).State = EntityState.Added;
                            }
                            else
                            {
                                // Update container return item
                                _context.Entry(containerReturnItem).State = EntityState.Modified;
                            }
                        }

                        // Assign default values to container
                        container.ReturnDate = order.OrderDate;
                        container.CustomerCode = order.CustomerCode;
                        container.PresaleRoute = order.PresaleRoute;
                        container.DeliveryRoute = order.DeliveryRoute;
                        container.SynchronizationDate = order.SynchronizationDate;
                        container.IsReturnFinished = false;
                        container.IsPresale = true;

                        // Update container
                        _context.Entry(container).State = EntityState.Modified;
                    }

                    // Update order
                    _context.Entry(order).State = EntityState.Modified;

                    // Save order changes
                    await _context.SaveChangesAsync();

                    // Add updated order to the list
                    updatedOrders.Add(new { order });
                }

                return StatusCode(201, new { OrdersWithoutErrors = updatedOrders, OrdersWithErrors = errorOrders });
            }
            else
            {
                return BadRequest("Orders can not be empty");
            }
        }

        // POST: api/v1/Controllers/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] List<Models.Order> orders)
        {
            // Verify whether any order was sent to the server
            if (orders.Any())
            {
                // Get delivery routes
                List<DeliveryRoute> deliveryRoutes = _context.DeliveryRoutes.ToList();

                // Make new order helper instance
                Helpers.Order orderHelper = new Helpers.Order(deliveryRoutes);

                // Create new empty list to save orders that were already processed to the server
                List<Object> savedOrders = new List<Object>();

                // Create new empty list to save orders that could not be processed to the server
                List<Object> errorOrders = new List<Object>();

                // Get current server date
                var now = DateTime.Now;

                // Iterate through all orders sent from web/mobile application
                foreach (var order in orders)
                {
                    if (_context.Orders.Any(x => x.OrderNumber == order.OrderNumber))
                    {
                        // Add order to error list if order number already exists
                        errorOrders.Add(new { order, error = $"Order number {order.OrderNumber} already exists" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get order items
                    List<OrderItem> items = order.Items;

                    // Verify whether the order has items
                    if (items == null || !items.Any())
                    {
                        // Add order to error list if not any item was specified
                        errorOrders.Add(new { order, error = "Items can not be empty" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get order container
                    ContainerReturn container = order.Container;

                    // Verify whether containers list is not empty
                    if (container != null)
                    {
                        // Assign default values to container
                        container.ReturnDate = order.OrderDate;
                        container.CustomerCode = order.CustomerCode;
                        container.PresaleRoute = order.PresaleRoute;
                        container.DeliveryRoute = order.DeliveryRoute;
                        container.SynchronizationDate = now;
                        container.IsReturnFinished = false;
                        container.IsPresale = true;

                        // Add container to the context
                        _context.ContainerReturns.Add(container);
                    }

                    // Set default values to order instance
                    order.SynchronizationDate = now;
                    order.IsOrderConfirmed = true;
                    order.IsOrderFinished = false;
                    order.DeliveryDate = orderHelper.CalculateDeliveryDate(order.DeliveryRoute, now);

                    // Add order to the context
                    _context.Orders.Add(order);

                    // Save order changes
                    await _context.SaveChangesAsync();

                    // Add saved order to the list
                    savedOrders.Add(new { order });
                }

                return StatusCode(201, new { OrdersWithoutErrors = savedOrders, OrdersWithErrors = errorOrders });
            }
            else
            {
                return BadRequest("Orders can not be empty");
            }
        }

        // DELETE: api/v1/Controllers/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}