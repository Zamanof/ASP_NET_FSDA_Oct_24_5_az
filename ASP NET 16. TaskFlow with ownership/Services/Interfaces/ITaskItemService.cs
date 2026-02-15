using ASP_NET_16._TaskFlow_with_ownership.Models;
using ASP_NET_16._TaskFlow_with_ownership.DTOs.TaskItem_DTOs;
using ASP_NET_16._TaskFlow_with_ownership.Common;
using ASP_NET_16._TaskFlow_with_ownership.DTOs;

namespace ASP_NET_16._TaskFlow_with_ownership.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItem?> GetTaskEntityAsync(int id);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTaskItem);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTaskItem);
    Task<TaskItemResponseDto?> UpdateStatusAsync(int id, TaskStatusUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
