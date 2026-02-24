namespace ASP_NET_22._TaskFlow_CQRS.Domain;

public class ProjectMember
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
}
