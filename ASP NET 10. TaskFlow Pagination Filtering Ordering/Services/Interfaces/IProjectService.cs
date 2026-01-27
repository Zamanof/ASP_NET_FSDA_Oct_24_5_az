using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Models;
using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.DTOs.Project_DTOs;

namespace ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
}
