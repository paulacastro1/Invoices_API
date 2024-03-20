using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing_API_Net8.Models
{
    public class CurrencyResource
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Description { get; set; }

}
}