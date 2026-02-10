using ASP_NET_16._TaskFlow_with_ownership.Models;
using ASP_NET_16._TaskFlow_with_ownership.DTOs.Project_DTOs;

namespace ASP_NET_16._TaskFlow_with_ownership.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
