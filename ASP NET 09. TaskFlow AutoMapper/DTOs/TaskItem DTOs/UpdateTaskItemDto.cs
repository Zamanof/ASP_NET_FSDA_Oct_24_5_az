namespace ASP_NET_09._TaskFlow_AutoMapper.DTOs.TaskItem_DTOs;

using ASP_NET_09._TaskFlow_AutoMapper.Models;

public class UpdateTaskItemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
}
