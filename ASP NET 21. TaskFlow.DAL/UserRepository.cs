using ASP_NET_21._TaskFlow.Data;
using ASP_NET_21._TaskFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_21._TaskFlow.DAL;

public class UserRepository : IUserRepository
{
    private readonly TaskFlowDBContext _context;

    public UserRepository(TaskFlowDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> GetOrderedByEmailExceptIdsAsync(IEnumerable<string> excludeIds)
    {
        var ids = excludeIds.ToList();
        return await _context.Users
            .Where(u => !ids.Contains(u.Id))
            .OrderBy(u => u.Email)
            .ToListAsync();
    }
}
