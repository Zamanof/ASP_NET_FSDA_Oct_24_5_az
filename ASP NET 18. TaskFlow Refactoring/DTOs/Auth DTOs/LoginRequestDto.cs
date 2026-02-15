namespace ASP_NET_18._TaskFlow_Refactoring.DTOs.Auth_DTOs;

public class LoginRequestDto
{
    /// <summary>
    /// User Email
    /// </summary>
    /// <example>john@doe.com</example>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Password
    /// </summary>
    /// <example>P@ssword123!</example>
    public string Password { get; set; } = string.Empty;
}
