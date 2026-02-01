namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.Project_DTOs;

/// <summary>
/// DTO for create project
/// Use in POST requests
/// </summary>
public class CreateProjectDto
{
    /// <summary>
    /// Project name
    /// </summary>
    /// <example>My new project</example>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Project description
    /// </summary>
    /// <example>Description for my project</example>
    public string Description { get; set; } = string.Empty;
}
