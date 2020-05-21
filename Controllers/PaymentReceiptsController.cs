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
    public class PaymentReceiptsController : ControllerBase
    {
        private readonly DistributionContext _context;

        public PaymentReceiptsController(DistributionContext context)
        {
            _context = context;
        }

        // GET: api/v1/Controllers/PaymentReceipts
        [HttpGet]
        public IEnumerable<PaymentReceipt> GetPaymentReceipts()
        {
            return _context.PaymentReceipts;
        }

        // GET: api/v1/Controllers/PaymentReceipts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentReceipt([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentReceipt = await _context.PaymentReceipts.FindAsync(id);

            if (paymentReceipt == null)
            {
                return NotFound();
            }

            return Ok(paymentReceipt);
        }

        // PUT: api/v1/Controllers/PaymentReceipts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentReceipt([FromRoute] int id, [FromBody] PaymentReceipt paymentReceipt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentReceipt.Id)
            {
                return BadRequest();
            }

            _context.Entry(paymentReceipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentReceiptExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_context.PaymentReceipts.Find(id));
        }

        // POST: api/v1/Controllers/PaymentReceipts
        [HttpPost]
        public async Task<IActionResult> PostPaymentReceipt([FromBody] List<PaymentReceipt> paymentReceipts)
        {
            // Verify whether any invoice was sent to the server
            if (paymentReceipts.Any())
            {
                // Create new empty list to saved success receipts that will be processed to the server
                List<PaymentReceipt> successReceipts = new List<PaymentReceipt>();

                // Create new empty list to save receipts that could not be processed to the server
                List<Object> errorReceipts = new List<Object>();

                // Iterate through all receipts sent
                foreach (var receipt in paymentReceipts)
                {
                    // Verify whether receipt number is not duplicated
                    if (_context.PaymentReceipts.Any(x => x.ReceiptNumber == receipt.ReceiptNumber))
                    {
                        // Add order to error list if receipt number already exists
                        errorReceipts.Add(new { receipt, error = $"Receipt number {receipt.ReceiptNumber} already exists" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Get receipt items
                    List<PaymentReceiptItem> items = receipt.Items;

                    // Verify whether the receipt has items
                    if (items == null || !items.Any())
                    {
                        // Add receipt to error list if not any item was specified
                        errorReceipts.Add(new { receipt, error = "Items can not be empty" });

                        // Continue with the next iteration
                        continue;
                    }

                    // Add success invoice to the list
                    successReceipts.Add(receipt);
                }

                // Create new empty list to save receipts that will be sent to the server
                List<Object> savedReceipts = new List<Object>();

                // Verify whether success receipts list is not empty
                if (successReceipts.Any())
                {
                    // Create a new synchronization record
                    SynchronizationRecord synchronizationRecord = new SynchronizationRecord
                    {
                        Route = successReceipts.FirstOrDefault().Route,
                        TypeOfCatalogId = "7", // Receipt
                        TypeOfVisitId = "2", // Delivery
                        FileName = "api.v1.paymentreceipts.postpaymentreceipts",
                        SynchronizationDate = DateTime.Now,
                        RecordsRead = successReceipts.Count,
                        RecordsSynchronized1 = successReceipts.Count,
                        RecordsSynchronized2 = successReceipts.Count,
                        ProcessType = Enumerations.ProcessType.Upload
                    };

                    // Add synchronization record to the context
                    _context.SynchronizationRecords.Add(synchronizationRecord);

                    // Save synchronization record to the database
                    await _context.SaveChangesAsync();

                    // Iterate through success receipts
                    foreach (var receipt in successReceipts)
                    {
                        // Set default values to receipt instance
                        receipt.SynchronizationDate = DateTime.Now;
                        receipt.SynchronizationId = synchronizationRecord.Id;

                        // Add receipt to the context
                        _context.PaymentReceipts.Add(receipt);

                        // Save invoice changes
                        await _context.SaveChangesAsync();

                        // Add saved receipt to the list
                        savedReceipts.Add(new { receipt });
                    }

                    // Get max receipt sequence
                    int lastReceiptSequence = paymentReceipts
                        .Select(x => Int32.Parse(x.ReceiptNumber.Split("-")[1]))
                        .Max();

                    // Get last receipt number
                    string lastReceiptNumber = paymentReceipts
                        .Where(x => x.ReceiptNumber == $"{paymentReceipts.First().Route}-{lastReceiptSequence.ToString()}")
                        .Select(x => x.ReceiptNumber)
                        .FirstOrDefault();

                    // Get current consecutive receipt number
                    ConsecutiveInvoice consecutiveInvoice = _context
                        .ConsecutiveInvoices
                        .Where(x => x.Route == paymentReceipts.First().Route)
                        .Where(x => x.DocumentTypeId == "2")
                        .FirstOrDefault();

                    // Verify whether last receipt number from receipts list is greater than last consecutive receipt number
                    if (lastReceiptSequence > consecutiveInvoice.SequenceNumber)
                    {
                        consecutiveInvoice.SequenceNumber = lastReceiptSequence;
                    }
                    else
                    {
                        consecutiveInvoice.SequenceNumber += 1;
                    }

                    consecutiveInvoice.LastInvoiceNumber = lastReceiptNumber;
                    consecutiveInvoice.UpdatedAt = DateTime.Now;

                    // Update consecutive invoice
                    _context.Entry(consecutiveInvoice).State = EntityState.Modified;

                    // Save changes to database
                    await _context.SaveChangesAsync();
                }

                return StatusCode(201, new { ReceiptsWithoutErrors = savedReceipts, ReceiptsWithErrors = errorReceipts });
            }
            else
            {
                return BadRequest("Payment receipts can not be empty");
            }
        }

        // DELETE: api/v1/Controllers/PaymentReceipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentReceipt([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentReceipt = await _context.PaymentReceipts.FindAsync(id);
            if (paymentReceipt == null)
            {
                return NotFound();
            }

            _context.PaymentReceipts.Remove(paymentReceipt);
            await _context.SaveChangesAsync();

            return Ok(paymentReceipt);
        }

        private bool PaymentReceiptExists(int id)
        {
            return _context.PaymentReceipts.Any(e => e.Id == id);
        }
    }
}