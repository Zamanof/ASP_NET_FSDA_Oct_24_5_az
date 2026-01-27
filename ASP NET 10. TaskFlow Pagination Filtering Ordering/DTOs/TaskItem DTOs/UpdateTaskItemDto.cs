namespace ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.DTOs.TaskItem_DTOs;

using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Models;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
}
