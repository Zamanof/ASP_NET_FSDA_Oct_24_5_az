using ASP_NET_08._TaskFLow_DTO.Models;
using ASP_NET_08._TaskFLow_DTO.DTOs.TaskItem_DTOs;

namespace ASP_NET_08._TaskFLow_DTO.Services.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItemResponseDto>> GetAllAsync();
    Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId);
    Task<TaskItemResponseDto?> GetByIdAsync(int id);
    Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTaskItem);
    Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTaskItem);
    Task<bool> DeleteAsync(int id);
}
