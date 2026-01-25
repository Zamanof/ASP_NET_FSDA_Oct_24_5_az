using ASP_NET_09._TaskFlow_Swagger_documentation.Data;
using ASP_NET_09._TaskFlow_Swagger_documentation.Models;
using ASP_NET_09._TaskFlow_Swagger_documentation.Services.Interfaces;
using ASP_NET_09._TaskFlow_Swagger_documentation.DTOs.TaskItem_DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ASP_NET_09._TaskFlow_Swagger_documentation.Services;

public class TaskItemService : ITaskItemService
{
    private readonly TaskFlowDBContext _context;
    private readonly IMapper _mapper;

    public TaskItemService(TaskFlowDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskItemResponseDto> CreateAsync(CreateTaskItemDto createTaskItem)
    {
        var isProjectExists = await _context
                                        .Projects
                                        .AnyAsync(p => p.Id == createTaskItem.ProjectId);

        if (!isProjectExists)
            throw new ArgumentException($"Project with ID {createTaskItem.ProjectId} not found");


        var taskItem = _mapper.Map<TaskItem>(createTaskItem);



        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();

        await _context
                    .Entry(taskItem)
                    .Reference(t => t.Project)
                    .LoadAsync();

        return _mapper.Map<TaskItemResponseDto>(taskItem);
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
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }

    public async Task<TaskItemResponseDto?> GetByIdAsync(int id)
    {
        var task = await _context
                          .TaskItems
                          .Include(t => t.Project)
                          .FirstOrDefaultAsync(t => t.Id == id);
        return _mapper.Map<TaskItemResponseDto>(task);
    }

    public async Task<IEnumerable<TaskItemResponseDto>> GetByProjectIdAsync(int projectId)
    {
        var tasks = await _context
                          .TaskItems
                          .Include(t => t.Project)
                          .Where(t => t.ProjectId == projectId)
                          .ToListAsync();
        return _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);
    }

    public async Task<TaskItemResponseDto?> UpdateAsync(int id, UpdateTaskItemDto updateTaskItem)
    {
        var task = await _context
                             .TaskItems
                             .Include(t => t.Project)
                             .FirstOrDefaultAsync(t => t.Id == id);
        if (task is null) return null;

        _mapper.Map(updateTaskItem, task);

        await _context.SaveChangesAsync();

        return _mapper.Map<TaskItemResponseDto>(task);
    }
    
}
