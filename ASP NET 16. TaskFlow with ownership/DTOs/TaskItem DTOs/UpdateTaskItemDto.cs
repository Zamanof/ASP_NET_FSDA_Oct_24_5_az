namespace ASP_NET_16._TaskFlow_with_ownership.DTOs.TaskItem_DTOs;

using ASP_NET_16._TaskFlow_with_ownership.Models;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
}
