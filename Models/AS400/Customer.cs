using System;
using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models.AS400
{
    public class Customer
    {
        [Key]
        public int CustomerCode { get; set; }
        public string RUCNumber { get; set; }
        public string CustomerName { get; set; }
        public string BusinessName { get; set; }
        public string BusinessProfile { get; set; }
        public string BusinessChannelCode { get; set; }
        public string BusinessChannelName { get; set; }
        public string CustomerTypeCode { get; set; }
        public string CustomerTypeName { get; set; }
        public string Department { get; set; }
        public string Municipality { get; set; }
        public string Neighborhood { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CustomerLocation { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditType { get; set; }
        public Decimal CreditLimit { get; set; }
        public string CreditDays { get; set; }
        public string PresaleRouteCode { get; set; }
        public string PresaleRouteName { get; set; }
        public string DeliveryRouteCode { get; set; }
        public string DeliveryRouteName { get; set; }
        public string VisitDaysCode { get; set; }
        public string VisitDaysName { get; set; }
        public string SequenceOfVisit { get; set; }
        public string SellingAreaCode { get; set; }
        public string SellingAreaName { get; set; }
        public int CorporateFather { get; set; }
        public Decimal PercentageOfCentralization { get; set; }
    }
}