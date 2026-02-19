using ASP_NET_20._TaskFlow_FIle_attachment.Common;
using ASP_NET_20._TaskFlow_FIle_attachment.Data;
using ASP_NET_20._TaskFlow_FIle_attachment.DTOs;
using ASP_NET_20._TaskFlow_FIle_attachment.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_20._TaskFlow_FIle_attachment.Services;

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

    public async Task<PagedResult<TaskItemResponseDto>> GetPagedAsync(TaskItemQueryParams queryParams)
    {
        queryParams.Validate();

        var query = _context
                        .TaskItems
                        .Include(t => t.Project)
                        .AsQueryable();

        if (queryParams.ProjectId.HasValue)
            query = query.Where(t => t.ProjectId == queryParams.ProjectId.Value);

        if (!string.IsNullOrWhiteSpace(queryParams.Status))
        {
            if (Enum.TryParse<Models.TaskStatus>(queryParams.Status, out var status))
            {
                query = query.Where(t => t.Status == status);
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Priority))
        {
            if (Enum.TryParse<Models.TaskPriority>(queryParams.Priority, out var priority))
            {
                query = query.Where(t => t.Priority == priority);
            }
        }

        if (!string.IsNullOrWhiteSpace(queryParams.Search))
        {
            var searchTerm = queryParams.Search.ToLower();
            query = query.Where(
                t => t.Title.ToLower().Contains(searchTerm) ||
                    (t.Description != null && t.Description.ToLower().Contains(searchTerm)));
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(queryParams.Sort))
        {
            query = ApplySorting(query, queryParams.Sort, queryParams.SortDirection);
        }
        else
        {
            query = query.OrderByDescending(t => t.CreatedAt);
        }

        var skip = (queryParams.Page - 1) * queryParams.Size;
        var tasks = await query
                            .Skip(skip)
                            .Take(queryParams.Size)
                            .ToListAsync();
        var taskDtos = _mapper.Map<IEnumerable<TaskItemResponseDto>>(tasks);

        return PagedResult<TaskItemResponseDto>.Create(
            taskDtos,
            queryParams.Page,
            queryParams.Size,
            totalCount
            );
    }

    private IQueryable<TaskItem> ApplySorting(
        IQueryable<TaskItem> query, 
        string sort, 
        string? sortDirection)
    {
        var isDescending = sortDirection?.ToLower() == "desc";

        return sort.ToLower() switch
        {
            "title"=> isDescending
                    ?query.OrderByDescending(t=>t.Title)
                    :query.OrderBy(t=>t.Title),
            "createdat"=> isDescending
                    ?query.OrderByDescending(t=>t.CreatedAt)
                    :query.OrderBy(t=>t.CreatedAt),
            "status"=> isDescending
                    ?query.OrderByDescending(t=>t.Status)
                    :query.OrderBy(t=>t.Status),
            "priority"=> isDescending
                    ?query.OrderByDescending(t=>t.Priority)
                    :query.OrderBy(t=>t.Priority),
            _ => query.OrderBy(t=> t.CreatedAt)
        };
    }

    public async Task<TaskItemResponseDto?> UpdateStatusAsync(int id, TaskStatusUpdateRequest request)
    {
        var task = await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
            return null;

        task.Status = request.Status;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return _mapper.Map<TaskItemResponseDto?>(task);

    }

    public async Task<TaskItem?> GetTaskEntityAsync(int id)
    {
        return await _context
                            .TaskItems
                            .Include(t => t.Project)
                            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
