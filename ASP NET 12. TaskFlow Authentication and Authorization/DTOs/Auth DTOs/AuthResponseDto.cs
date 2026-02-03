namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.Auth_DTOs;

public class AuthResponseDto
{
    /// <summary>
    /// Access token
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
    /// <summary>
    /// Token Expirens date 
    /// </summary>
    public DateTime ExpiresAt { get; set; }
    /// <summary>
    /// User Email
    /// </summary>
    /// <example>john@doe.com</example>
    public string Email { get; set; } = string.Empty;

    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
