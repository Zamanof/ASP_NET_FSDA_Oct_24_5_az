namespace ASP_NET_18._TaskFlow_Refactoring.DTOs.TaskItem_DTOs;

using ASP_NET_18._TaskFlow_Refactoring.Models;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
}
