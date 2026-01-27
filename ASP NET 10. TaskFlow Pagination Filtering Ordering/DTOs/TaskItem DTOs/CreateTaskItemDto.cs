using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Models;

namespace ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.DTOs.TaskItem_DTOs;

public class CreateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public int ProjectId { get; set; }
}
