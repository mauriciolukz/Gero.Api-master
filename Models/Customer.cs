using System.ComponentModel.DataAnnotations;

namespace Gero.API.Models
{
    public class Customer
    {
        [Key]
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }
}
