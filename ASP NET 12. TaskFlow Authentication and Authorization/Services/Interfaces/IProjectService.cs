using ASP_NET_12._TaskFlow_Authentication_and_Authorization.Models;
using ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.Project_DTOs;

namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
