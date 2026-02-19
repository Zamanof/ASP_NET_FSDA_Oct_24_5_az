using ASP_NET_20._TaskFlow_FIle_attachment.Models;
using ASP_NET_20._TaskFlow_FIle_attachment.DTOs;

namespace ASP_NET_20._TaskFlow_FIle_attachment.Services;

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
