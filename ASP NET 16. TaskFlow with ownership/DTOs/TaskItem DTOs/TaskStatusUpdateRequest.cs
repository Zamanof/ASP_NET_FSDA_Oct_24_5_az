using ASP_NET_16._TaskFlow_with_ownership.Models;
using TaskStatus = ASP_NET_16._TaskFlow_with_ownership.Models.TaskStatus;

namespace ASP_NET_16._TaskFlow_with_ownership.DTOs.TaskItem_DTOs;

public class TaskStatusUpdateRequest
{
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
}
