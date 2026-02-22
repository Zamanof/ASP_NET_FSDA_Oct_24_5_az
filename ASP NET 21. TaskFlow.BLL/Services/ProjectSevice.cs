using ASP_NET_21._TaskFlow.BLL.DTOs;
using ASP_NET_21._TaskFlow.DAL;
using ASP_NET_21._TaskFlow.Data;
using ASP_NET_21._TaskFlow.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_21._TaskFlow.BLL.Services;

public class ProjectSevice : IProjectService
{
    private readonly TaskFlowDBContext _context;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProjectSevice(IProjectRepository projectRepository, IMapper mapper, UserManager<ApplicationUser> userManager, TaskFlowDBContext context)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllForUserAsync(
        string userId, IList<string> roles)
    {
        var projects = await _projectRepository.GetAllForUserAsync(userId, roles);

        return _mapper.Map<IEnumerable<ProjectResponseDto>>(projects);
    }

    public async Task<Project?> GetProjectEntityAsync(int id)
    {
        return await _projectRepository.GetByIdWithTasksAndMembersAsync(id);
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdWithTasksAsync(id);

        if (project is null) return null;

        return _mapper.Map<ProjectResponseDto>(project);
    }
    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto createProjectDto, string ownerId)
    {
        var project = _mapper.Map<Project>(createProjectDto);
        project.OwnerId = ownerId;

        await _projectRepository.AddAsync(project);

        return _mapper.Map<ProjectResponseDto>(project);
    }

    public async Task<ProjectResponseDto?> UpdateAsync(int id, UpdateProjectDto updateProjectDto)
    {
        var updatedProject = await _projectRepository.GetByIdWithTasksAsync(id);

        if (updatedProject is null) return null;

        _mapper.Map(updateProjectDto, updatedProject);

       await _projectRepository.UpdateAsync(updatedProject);

        return _mapper.Map<ProjectResponseDto>(updatedProject);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _projectRepository.FindAsync(id);
        if (project is null) 
            return false;
        await _projectRepository.RemoveAsync(project);
        return true;
    }

    public async Task<IEnumerable<ProjectMemberResponseDto>> GetMembersAsync(int projectId)
    {
        var members = await _context.ProjectMembers
                                   .Include(m => m.User)
                                   .Where(m => m.ProjectId == projectId)
                                   .OrderBy(m => m.CreatedAt)
                                   .ToListAsync();

        return members.Select(m => new ProjectMemberResponseDto
        {
            UserId = m.UserId,
            Email = m.User.Email!,
            FirstName = m.User.FirstName,
            LastName = m.User.LastName,
            JoinedAt = m.CreatedAt
        });
    }

    public async Task<IEnumerable<AvailableUserDto>> GetAvailableUsersToAddAsync(int projectId)
    {
        var memberUserIds = await _context.ProjectMembers
                                    .Where(m => m.ProjectId == projectId)
                                    .Select(m => m.UserId)
                                    .ToListAsync();
        var users = await _context.Users
                                  .Where(u => !memberUserIds.Contains(u.Id))
                                  .OrderBy(u => u.Email)
                                  .Select(u => new AvailableUserDto
                                  {
                                      Id = u.Id,
                                      Email = u.Email!,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName
                                  })
                                  .ToListAsync();
        return users;
    }

    public async Task<bool> AddMemberAsync(int projectId, string userIdOrEmail)
    {
        var project = await _context.Projects.FindAsync(projectId);
        
        if (project is null) return false;
        
        ApplicationUser? user = null;

        if(userIdOrEmail.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(userIdOrEmail);
        }
        else
        {
            user = await _userManager.FindByIdAsync(userIdOrEmail);
        }

        if(await _context.ProjectMembers
            .AnyAsync(m => m.ProjectId == projectId && m.UserId == user!.Id))
            return false;

        _context.ProjectMembers.Add(new ProjectMember
        {
            ProjectId = projectId,
            UserId = user!.Id,
            CreatedAt = DateTimeOffset.UtcNow
        });

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveMemberAsync(int projectId, string userId)
    {
        var member = await _context.ProjectMembers
                        .FirstOrDefaultAsync(m => m.ProjectId == projectId && m.UserId == userId);
        
        if (member is null) return false;

        _context.ProjectMembers.Remove(member);

        await _context.SaveChangesAsync();

        return true;
    }

}
