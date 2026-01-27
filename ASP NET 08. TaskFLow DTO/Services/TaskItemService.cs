using ASP_NET_08._TaskFLow_DTO.Data;
using ASP_NET_08._TaskFLow_DTO.Models;
using ASP_NET_08._TaskFLow_DTO.Services.Interfaces;
using ASP_NET_08._TaskFLow_DTO.DTOs.TaskItem_DTOs;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_08._TaskFLow_DTO.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDBContext _context;

    public TaskItemService(TaskFlowDBContext context)
    {
        _context = context;
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTaskItem)
    {
        var isProjectExists = await _context
                                        .Projects
                                        .AnyAsync(p => p.Id == createTaskItem.ProjectId);

        if (!isProjectExists)
            throw new ArgumentException($"Project with ID {createTaskItem.ProjectId} not found");


        var taskItem = new TaskItem
        {
            Title = createTaskItem.Title,
            Description = createTaskItem.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            Status = Models.TaskStatus.ToDo,
            ProjectId = createTaskItem.ProjectId
        };



        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        await _context
                    .Entry(taskItem)
                    .Reference(t => t.Project)
                    .LoadAsync();

        return MapToResponseDto(taskItem);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);

        if (task is null) return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
    {
        var tasks = await _context
                           .TaskItems
                           .Include(t => t.Project)
                           .ToListAsync();
        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(int id)
    {
        var task = await _context
                          .TaskItems
                          .Include(t => t.Project)
                          .FirstOrDefaultAsync(t => t.Id == id);
        return MapToResponseDto(task!);
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _context
                          .TaskItems
                          .Include(t => t.Project)
                          .Where(t => t.ProjectId == projectId)
                          .ToListAsync();
        return tasks.Select(MapToResponseDto);
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTaskItem)
    {
        var task = await _context
                             .TaskItems
                             .Include(t => t.Project)
                             .FirstOrDefaultAsync(t => t.Id == id);
        if (task is null) return null;

        task.Title = updateTaskItem.Title;
        task.Description = updateTaskItem.Description;
        task.Status = updateTaskItem.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponseDto(task);
    }

    private TaskItemResponseDto MapToResponseDto(TaskItem taskItem)
    {
        return new TaskItemResponseDto
        {
            Id = taskItem.Id,
            Title = taskItem.Title,
            Description = taskItem.Description,
            Status = taskItem.Status.ToString(),
            ProjectId = taskItem.ProjectId,
            ProjectName = taskItem.Project.Name
        };
    }
}
