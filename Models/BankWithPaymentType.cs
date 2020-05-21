using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gero.API.Models
{
    public class BankWithPaymentType
    {
        [Key]
        public Int64 Id { get; set; }
        public int BankCode { get; set; }
        public string BankName { get; set; }
        public int PaymentTypeCode { get; set; }
        public string PaymentTypeName { get; set; }
        public string DepositBank { get; set; }
        public string ForeignDepositBank { get; set; }
        public string CurrencyCode { get; set; }
    }
}