using ASP_NET_08._TaskFLow_DTO.Data;
using ASP_NET_08._TaskFLow_DTO.Models;
using ASP_NET_08._TaskFLow_DTO.Services.Interfaces;
using ASP_NET_08._TaskFLow_DTO.DTOs.Project_DTOs;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_08._TaskFLow_DTO.Services;

public class ProjectSevice : IProjectService
{
    private readonly TaskFlowDBContext _context;

    public ProjectSevice(TaskFlowDBContext context)
    {
        _context = context;
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto createProjectDto)
    {
        var project = new Project();
        project.Name = createProjectDto.Name;
        project.Description = createProjectDto.Description;
        project.CreatedAt = DateTimeOffset.UtcNow;
        project.UpdatedAt = null;

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        await _context
            .Entry(project)
            .Collection(p => p.Tasks)
            .LoadAsync();

        return MapToResponseDto(project);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project is null) return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        var projects = await _context
            .Projects
            .Include(p => p.Tasks)
            .ToListAsync();
        return projects.Select(p=>MapToResponseDto(p));
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        var project = await _context
            .Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return MapToResponseDto(project!);
    }

    public async Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto)
    {
        var updatedProject = await _context
                                    .Projects
                                    .Include(p => p.Tasks)
                                    .FirstOrDefaultAsync(p => p.Id == id);

        if (updatedProject is null) return null;

        updatedProject.Name = updateProjectDto.Name;
        updatedProject.Description = updateProjectDto.Description;
        updatedProject.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        return MapToResponseDto(updatedProject);
    }

    private ProjectResponseDto MapToResponseDto(Project project)
    {
        return new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            TaskCount = project.Tasks.Count()
        };
    }
}
