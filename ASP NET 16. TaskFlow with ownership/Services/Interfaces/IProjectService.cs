using ASP_NET_16._TaskFlow_with_ownership.Models;
using ASP_NET_16._TaskFlow_with_ownership.DTOs.Project_DTOs;

namespace ASP_NET_16._TaskFlow_with_ownership.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllForUserAsync(string userId, IList<string> roles);
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<Project?> GetProjectEntityAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto  createProjectDto, string ownerId);
    Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto  updateProjectDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ProjectMemberResponseDto>> GetMembersAsync(int projectId);
    Task<IEnumerable<AvailableUserDto>> GetAvailableUsersToAddAsync(int projectId);
    Task<bool> AddMemberAsync(int projectId, string userIdOrEmail);
    Task<bool> RemoveMemberAsync(int projectId, string userId);

}
