using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using ASP_NET_22._TaskFlow_CQRS.Application.Interfaces;
using ASP_NET_22._TaskFlow_CQRS.Domain;
using AutoMapper;

namespace ASP_NET_22._TaskFlow_CQRS.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAuthUserStore _authUserStore;

    public ProjectService(
        IProjectRepository projectRepository,
        IProjectMemberRepository projectMemberRepository,
        IUserRepository userRepository,
        IAuthUserStore authUserStore)
    {
        _projectRepository = projectRepository;
        _projectMemberRepository = projectMemberRepository;
        _userRepository = userRepository;
        _authUserStore = authUserStore;
    }

    public async Task<Project?> GetProjectEntityAsync(int id) =>
        await _projectRepository.GetByIdWithTasksAndMembersAsync(id);

    public async Task<IEnumerable<ProjectMemberResponseDto>> GetMembersAsync(int projectId) =>
        await _projectMemberRepository.GetByProjectIdWithUserAsync(projectId);

    public async Task<IEnumerable<AvailableUserDto>> GetAvailableUsersToAddAsync(int projectId)
    {
        var memberUserIds = await _projectMemberRepository.GetMemberUserIdsAsync(projectId);
        return await _userRepository.GetOrderedByEmailExceptIdsAsync(memberUserIds);
    }

    public async Task<bool> AddMemberAsync(int projectId, string userIdOrEmail)
    {
        var project = await _projectRepository.FindAsync(projectId);
        if (project is null) return false;

        var userId = await _authUserStore.FindUserIdByEmailOrIdAsync(userIdOrEmail);
        if (userId is null) return false;
        if (await _projectMemberRepository.ExistsAsync(projectId, userId)) return false;

        await _projectMemberRepository.AddAsync(new ProjectMember
        {
            ProjectId = projectId,
            UserId = userId,
            CreatedAt = DateTimeOffset.UtcNow
        });
        return true;
    }

    public async Task<bool> RemoveMemberAsync(int projectId, string userId)
    {
        var member = await _projectMemberRepository.FindAsync(projectId, userId);
        if (member is null) return false;
        await _projectMemberRepository.RemoveAsync(member);
        return true;
    }

    public Task<bool> IsMemberAsync(int projectId, string userId) =>
        _projectMemberRepository.ExistsAsync(projectId, userId);
}
