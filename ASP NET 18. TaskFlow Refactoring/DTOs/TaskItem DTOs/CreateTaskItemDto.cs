using ASP_NET_18._TaskFlow_Refactoring.Models;

namespace ASP_NET_18._TaskFlow_Refactoring.DTOs.TaskItem_DTOs;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
