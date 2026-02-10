using ASP_NET_16._TaskFlow_with_ownership.Models;

namespace ASP_NET_16._TaskFlow_with_ownership.DTOs.TaskItem_DTOs;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
