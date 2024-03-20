using Billing_API_NET8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billing_API_NET8.Entities
{
    public class SystemCurrentUserDto
    {
        public SystemUser CurrentUser { get; set; }

        //public SystemRolePermission CurrentRolePermission { get; set; }
    }
}
