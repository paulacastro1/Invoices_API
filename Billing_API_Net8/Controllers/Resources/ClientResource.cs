namespace Billing_API_Net8.Controllers.Resources
{
    public class ClientResource
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Balance { get; set; }
    }
}
