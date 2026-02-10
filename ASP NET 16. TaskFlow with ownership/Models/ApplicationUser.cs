using Microsoft.AspNetCore.Identity;

namespace ASP_NET_16._TaskFlow_with_ownership.Models;

public class ApplicationUser: IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } = null;
}

// OAuth 2.0
