using ASP_NET_14._TaskFlow_Refresh_Token.Models;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Project_DTOs;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
