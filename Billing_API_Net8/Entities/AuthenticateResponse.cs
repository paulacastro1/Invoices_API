using Billing_API_Net8.Entities;
using Billing_API_NET8.Models;

namespace Billing_API_NET8.Entities;


public class AuthenticateResponse
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(SystemUser user, string token)
    {
        Id = user.Id;
        Role = user.Role;
        Token = token;
    }
}