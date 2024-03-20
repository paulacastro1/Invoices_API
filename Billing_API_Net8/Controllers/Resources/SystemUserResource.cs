using Billing_API_Net8.Entities;
using System.Text.Json.Serialization;

namespace Billing_API_NET8.Controllers
{
    public class SystemUserResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RegistrationTemporalCode { get; set; }
        public Guid? ForgotPasswordTemporalCode { get; set; }
        public Role Role { get; set; }
        public string PasswordHash { get; set; }

        //public Guid UserRoleId { get; set; }
        //[ForeignKey("UserRoleId")]
        //public virtual SystemUserRole UserRole { get; set; }
    }
}
