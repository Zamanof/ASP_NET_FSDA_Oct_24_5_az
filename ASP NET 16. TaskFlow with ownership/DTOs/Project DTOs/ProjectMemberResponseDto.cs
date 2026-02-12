namespace ASP_NET_16._TaskFlow_with_ownership.DTOs.Project_DTOs;

public class ProjectMemberResponseDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
}
