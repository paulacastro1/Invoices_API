using System.ComponentModel.DataAnnotations;

namespace Billing_API_Net8.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address{ get; set; }
        public decimal Balance { get; set; }
}

}