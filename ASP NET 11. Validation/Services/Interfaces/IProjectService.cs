using ASP_NET_11._Validation.Models;
using ASP_NET_11._Validation.DTOs.Project_DTOs;

namespace ASP_NET_11._Validation.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
