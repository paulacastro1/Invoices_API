using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Billing_API_Net8.Entities
{
    public class SystemUserForLoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public Guid? ForgotPasswordTemporalCode { get; set; }
    }
}
