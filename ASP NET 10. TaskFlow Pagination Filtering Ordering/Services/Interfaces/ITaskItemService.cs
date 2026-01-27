using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Models;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.DTOs.TaskItem_DTOs;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Common;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.DTOs;

namespace ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Services.Interfaces;

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
