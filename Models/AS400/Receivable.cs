using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class Receivable
    {
        [Key]
        public int InvoiceNumber { get; set; }
        public string InvoiceNumberFromSmartSales { get; set; }
        public int CustomerCode { get; set; }
        public int InvoiceDate { get; set; }
        public int DueDate { get; set; }
        public Decimal StartingAmount { get; set; }
        public Decimal Balance { get; set; }
        public bool IsCashInvoice { get; set; }
    }
}