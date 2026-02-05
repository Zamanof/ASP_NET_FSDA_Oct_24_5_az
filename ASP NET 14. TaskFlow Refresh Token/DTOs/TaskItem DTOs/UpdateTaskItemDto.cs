namespace ASP_NET_14._TaskFlow_Refresh_Token.DTOs.TaskItem_DTOs;

using ASP_NET_14._TaskFlow_Refresh_Token.Models;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
}
