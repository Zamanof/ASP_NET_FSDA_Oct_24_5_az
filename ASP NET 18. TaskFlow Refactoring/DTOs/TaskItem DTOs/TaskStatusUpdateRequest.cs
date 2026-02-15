using ASP_NET_18._TaskFlow_Refactoring.Models;
using TaskStatus = ASP_NET_18._TaskFlow_Refactoring.Models.TaskStatus;

namespace ASP_NET_18._TaskFlow_Refactoring.DTOs.TaskItem_DTOs;

public class TaskStatusUpdateRequest
{
    public TaskStatus Status { get; set; } = TaskStatus.ToDo;
}
