namespace ASP_NET_18._TaskFlow_Refactoring.DTOs;


public class UserWithRolesDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();

}

public class AssignRoleDto
{
    public string Role { get; set; } = string.Empty;
}