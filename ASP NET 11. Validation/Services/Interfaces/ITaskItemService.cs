using ASP_NET_11._Validation.Models;
using ASP_NET_11._Validation.DTOs.TaskItem_DTOs;
using ASP_NET_11._Validation.Common;
using ASP_NET_11._Validation.DTOs;

namespace ASP_NET_11._Validation.Services.Interfaces;

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
