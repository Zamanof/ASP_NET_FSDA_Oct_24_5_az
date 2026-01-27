using ASP_NET_09._TaskFlow_AutoMapper.Models;
using ASP_NET_09._TaskFlow_AutoMapper.DTOs.Project_DTOs;

namespace ASP_NET_09._TaskFlow_AutoMapper.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
