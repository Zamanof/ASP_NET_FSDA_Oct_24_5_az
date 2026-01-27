using ASP_NET_08._TaskFLow_DTO.Models;
using ASP_NET_08._TaskFLow_DTO.DTOs.Project_DTOs;

namespace ASP_NET_08._TaskFLow_DTO.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
