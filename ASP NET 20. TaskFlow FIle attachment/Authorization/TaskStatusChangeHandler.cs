using ASP_NET_20._TaskFlow_FIle_attachment.Data;
using ASP_NET_20._TaskFlow_FIle_attachment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP_NET_20._TaskFlow_FIle_attachment.Authorization
{
    public class TaskStatusChangeHandler
        : AuthorizationHandler<TaskStatusChangeRequirment, TaskItem>
    {
        private readonly TaskFlowDBContext _context;

        public TaskStatusChangeHandler(TaskFlowDBContext context)
        {
            _context = context;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            TaskStatusChangeRequirment requirement, 
            TaskItem resource)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return;

            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            var project = await _context.Projects
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == resource.ProjectId);

            if (project is null)
                return;

            if(context.User.IsInRole("Manager") && project.OwnerId == userId)
            {
                context.Succeed(requirement);
                return;
            }

            var isMemeber = await _context.ProjectMembers
                                    .AnyAsync(m => m.ProjectId == resource.ProjectId
                                    && m.UserId == userId);

            if (isMemeber)
                context.Succeed(requirement);
        }
    }
}
