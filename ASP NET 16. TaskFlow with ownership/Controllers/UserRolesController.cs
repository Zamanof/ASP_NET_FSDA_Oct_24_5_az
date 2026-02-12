using ASP_NET_16._TaskFlow_with_ownership.Common;
using ASP_NET_16._TaskFlow_with_ownership.DTOs.Auth_DTOs;
using ASP_NET_16._TaskFlow_with_ownership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_16._TaskFlow_with_ownership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy ="AdminOnly")]
    public class UserRolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private static readonly string[] AllowedRoles = new[] { "Admin", "Manager", "User" };

        public UserRolesController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserWithRolesDto>>>> GetAll()
        {
            var users = _userManager.Users.OrderBy(u => u.Email).ToList();

            var dtos = new List<UserWithRolesDto>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                dtos.Add(new UserWithRolesDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                });
            }

            return Ok(ApiResponse<IEnumerable<UserWithRolesDto>>.SuccessResponse(dtos));

        }
    }
}
