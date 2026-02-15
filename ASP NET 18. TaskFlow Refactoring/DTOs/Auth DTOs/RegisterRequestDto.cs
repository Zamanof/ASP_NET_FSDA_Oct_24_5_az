namespace ASP_NET_18._TaskFlow_Refactoring.DTOs.Auth_DTOs;

public class RegisterRequestDto
{
    /// <summary>
    /// User Name
    /// </summary>
    /// <example>John</example>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>
    /// User LastName
    /// </summary>
    /// <example>Doe</example>
    public string LastName { get; set; } = string.Empty;
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
    /// <summary>
    /// Confirmed Password
    /// </summary>
    /// <example>P@ssword123!</example>
    public string ConfirmPassword { get; set; } = string.Empty;
}
