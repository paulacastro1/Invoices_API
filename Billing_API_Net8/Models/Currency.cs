using System.ComponentModel.DataAnnotations;

namespace Billing_API_Net8.Models
{
    public class Currency
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

}