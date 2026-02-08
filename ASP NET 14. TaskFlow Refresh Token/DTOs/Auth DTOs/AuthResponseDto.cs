namespace ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;

public class AuthResponseDto
{
    /// <summary>
    /// Access token
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
    /// <summary>
    /// Token Expires date 
    /// </summary>
    public DateTimeOffset ExpiresAt { get; set; }
    /// <summary>
    /// Refresh token
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
    /// <summary>
    /// Token Expires date 
    /// </summary>
    public DateTimeOffset RefreshTokenExpiresAt { get; set; }

    /// <summary>
    /// User Email
    /// </summary>
    /// <example>john@doe.com</example>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// User Roles
    /// </summary>
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}
