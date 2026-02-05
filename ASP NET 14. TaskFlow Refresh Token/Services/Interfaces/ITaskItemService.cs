using ASP_NET_14._TaskFlow_Refresh_Token.Models;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.TaskItem_DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Common;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;

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
