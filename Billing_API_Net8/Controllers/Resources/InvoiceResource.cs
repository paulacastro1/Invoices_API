
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing_API_Net8.Models
{
    public class InvoiceResource
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Guid CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }
        public Guid ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }

}