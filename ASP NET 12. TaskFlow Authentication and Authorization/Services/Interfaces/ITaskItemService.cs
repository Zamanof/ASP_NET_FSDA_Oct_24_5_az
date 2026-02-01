using ASP_NET_12._TaskFlow_Authentication_and_Authorization.Models;
using ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.TaskItem_DTOs;
using ASP_NET_12._TaskFlow_Authentication_and_Authorization.Common;
using ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs;

namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTaskItem);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTaskItem);
    Task<bool> DeleteAsync(int id);
}
