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
    public class InvoicesController : ControllerBase
    {
        private readonly DistributionContext _context;

        public InvoicesController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/Invoices
        [HttpGet]
        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices;
        }

        // GET: api/v1/Controllers/Invoices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/v1/Controllers/Invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice([FromRoute] int id, [FromBody] Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.Invoices.Find(id));
        }

        // POST: api/v1/Controllers/Invoices
        [HttpPost]
        public async Task<IActionResult> PostInvoice([FromBody] List<Invoice> invoices)
        {
            // Verify whether any invoice was sent to the server
            if (invoices.Any())
            {
                // Create new empty list to saved success invoices that will be processed to the server
                List<Invoice> successInvoices = new List<Invoice>();

                // Create new empty list to save invoices that could not be processed to the server
                List<Object> errorInvoices = new List<Object>();

                // Iterate through all invoices sent
                foreach (var invoice in invoices)
                {
                    // Verify whether invoice number is not duplicated
                    if (_context.Invoices.Any(x => x.InvoiceNumber == invoice.InvoiceNumber))
                    {
                        // Add order to error list if invoice number already exists
                        errorInvoices.Add(new { invoice, error = $"Invoice number {invoice.InvoiceNumber} already exists" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Verify whether order number exists
                    if (!_context.Orders.Any(x => x.OrderNumber == invoice.OrderNumber))
                    {
                        // Add order to error list if invoice number already exists
                        errorInvoices.Add(new { invoice, error = $"Order number {invoice.OrderNumber} for invoice {invoice.InvoiceNumber} does not exist" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get invoice items
                    List<InvoiceItem> items = invoice.Items;

                    // Verify whether the invoice has items
                    if (items == null || !items.Any())
                    {
                        // Add invoice to error list if not any item was specified
                        errorInvoices.Add(new { invoice, error = "Items can not be empty" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Add success invoice to the list
                    successInvoices.Add(invoice);
                }

                // Create new empty list to save invoices that will be sent to the server
                List<Object> savedInvoices = new List<Object>();

                // Verify whether success invoices list is not empty
                if (successInvoices.Any())
                {
                    // Create a new synchronization record
                    SynchronizationRecord synchronizationRecord = new SynchronizationRecord
                    {
                        Route = successInvoices.FirstOrDefault().Route,
                        TypeOfCatalogId = "8", // Invoice
                        TypeOfVisitId = "2", // Delivery
                        FileName = "api.v1.invoices.postinvoices",
                        SynchronizationDate = DateTime.Now,
                        RecordsRead = successInvoices.Count,
                        RecordsSynchronized1 = successInvoices.Count,
                        RecordsSynchronized2 = successInvoices.Count,
                        ProcessType = Enumerations.ProcessType.Upload
                    };

                    // Add synchronization record to the context
                    _context.SynchronizationRecords.Add(synchronizationRecord);

                    // Save synchronization record to the database
                    await _context.SaveChangesAsync();

                    // Iterate through success invoices
                    foreach (var invoice in successInvoices)
                    {
                        // Set default values to invoice instance
                        invoice.SynchronizationDate = DateTime.Now;
                        invoice.IsInvoiceFinished = false;
                        invoice.IsInvoiceCanceled = false;
                        invoice.SynchronizationId = synchronizationRecord.Id;

                        // Add invoice to the context
                        _context.Invoices.Add(invoice);

                        // Get order by order number
                        Order order = _context
                            .Orders
                            .Where(x => x.OrderNumber == invoice.OrderNumber)
                            .FirstOrDefault();

                        // Verify whether order is not null
                        if (order != null)
                        {
                            // Set is order finished to true
                            order.IsOrderFinished = true;

                            // Add order to modified state
                            _context.Entry(order).State = EntityState.Modified;
                        }

                        // Save invoice changes
                        await _context.SaveChangesAsync();

                        // Add saved invoice to the list
                        savedInvoices.Add(new { invoice });
                    }

                    // Get max invoice number
                    int lastInvoiceSequence = invoices
                        .Select(x => Int32.Parse(x.InvoiceNumber.Split("-")[1]))
                        .Max();

                    // Get last invoice number
                    string lastInvoiceNumber = invoices
                        .Where(x => x.InvoiceNumber == $"{invoices.First().Route}-{lastInvoiceSequence.ToString()}")
                        .Select(x => x.InvoiceNumber)
                        .FirstOrDefault();

                    // Get current consecutive invoice number
                    ConsecutiveInvoice consecutiveInvoice = _context
                        .ConsecutiveInvoices
                        .Where(x => x.Route == invoices.First().Route)
                        .Where(x => x.DocumentTypeId == "1")
                        .FirstOrDefault();

                    // Verify whether last invoice number from invoices list is greater than last consecutive invoice number
                    if (lastInvoiceSequence > consecutiveInvoice.SequenceNumber)
                    {
                        consecutiveInvoice.SequenceNumber = lastInvoiceSequence;
                    }
                    else
                    {
                        consecutiveInvoice.SequenceNumber += 1;
                    }

                    consecutiveInvoice.LastInvoiceNumber = lastInvoiceNumber;
                    consecutiveInvoice.UpdatedAt = DateTime.Now;

                    // Update consecutive invoice
                    _context.Entry(consecutiveInvoice).State = EntityState.Modified;

                    // Save changes to database
                    await _context.SaveChangesAsync();
                }

                return StatusCode(201, new { InvoicesWithoutErrors = savedInvoices, InvoicesWithErrors = errorInvoices });
            }
            else
            {
                return BadRequest("Invoices can not be empty");
            }
        }

        [HttpPatch("cancel")]
        public async Task<IActionResult> CancelInvoice([FromBody] List<Invoice> invoices)
        {
            // Verify whether invoices list is not empty
            if (invoices.Any())
            {
                // Create new empty list to save invoices that were already processed to the server
                List<Object> savedInvoices = new List<Object>();

                // Create new empty list to save invoices that could not be processed to the server
                List<Object> errorInvoices = new List<Object>();

                // Iterate through all invoices
                foreach (var invoice in invoices)
                {
                    // Verify whether invoice exists
                    if (!_context.Invoices.Any(x => x.InvoiceNumber == invoice.InvoiceNumber && x.IsInvoiceFinished == false))
                    {
                        // Add invoice to error list
                        errorInvoices.Add(new { invoice, error = $"The invoice does not exist" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get authorization code
                    AuthorizationCode authorizationCode = invoice.AuthorizationCode;

                    // Verify whether authorization code is not null
                    if (authorizationCode == null)
                    {
                        // Add invoice to error list
                        errorInvoices.Add(new { invoice, error = "The authorization code can not be empty" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Verify whether authorization code exists
                    if (
                        !_context.AuthorizationCodes
                        .Any(
                            x =>
                                x.Route == authorizationCode.Route &&
                                x.Code == authorizationCode.Code &&
                                x.Status == Enumerations.Status.Active
                            )
                    )
                    {
                        // Add invoice to error list
                        errorInvoices.Add(new { invoice, error = "The authorization code provided does not exist" });
                    }

                    // Set default values to authorization code entity
                    authorizationCode.CustomerCode = invoice.CustomerCode;
                    authorizationCode.Entity = "DST_Factura";
                    authorizationCode.EntityId = invoice.Id;
                    authorizationCode.Status = Enumerations.Status.Used;
                    authorizationCode.UpdatedAt = DateTimeOffset.Now;

                    // Add authorization code to database context
                    _context.Entry(authorizationCode).State = EntityState.Modified;

                    // Set is canceled to true to invoice entity
                    invoice.IsInvoiceCanceled = true;

                    // Add invoice to database context
                    _context.Entry(invoice).State = EntityState.Modified;

                    // Save changes to database
                    await _context.SaveChangesAsync();

                    // Add invoice to success list
                    savedInvoices.Add(new { invoice });
                }

                return StatusCode(201, new { InvoicesWithoutErrors = savedInvoices, InvoicesWithErrors = errorInvoices });
            }
            else
            {
                return BadRequest("Invoices can not be empty");
            }
        }

        // DELETE: api/v1/Controllers/Invoices/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}