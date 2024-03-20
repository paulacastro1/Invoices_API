using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Billing_API_Net8.Entities
{
    public class SystemUserForRegisterDto
    {
        [Required]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 20 characters")]
        public string? Password { get; set; }
        public Guid? UserRoleId { get; set; }
        public Guid? ForgotPasswordTemporalCode { get; set; }
        public string? Cellphone { get; set; }
        public string? RegistrationTemporalCode { get; set; }

    }
}
