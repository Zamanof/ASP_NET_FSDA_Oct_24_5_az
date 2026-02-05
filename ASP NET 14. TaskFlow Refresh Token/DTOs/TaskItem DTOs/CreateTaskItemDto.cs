using ASP_NET_14._TaskFlow_Refresh_Token.Models;

namespace ASP_NET_14._TaskFlow_Refresh_Token.DTOs.TaskItem_DTOs;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
