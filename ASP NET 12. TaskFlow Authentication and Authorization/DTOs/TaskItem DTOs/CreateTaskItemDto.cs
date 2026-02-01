using ASP_NET_12._TaskFlow_Authentication_and_Authorization.Models;

namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.TaskItem_DTOs;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
