using ASP_NET_14._TaskFlow_Refresh_Token.Data;
using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;
using ASP_NET_14._TaskFlow_Refresh_Token.Models;
using ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly TaskFlowDBContext _context;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        TaskFlowDBContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));

            throw new InvalidOperationException($"User creation failed: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var (principial, jti) = ValidateRefreshJwtAndGetJti(refreshTokenRequest.RefreshToken);
        var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.JwtId == jti);

        if (storedToken is null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if(!storedToken.IsActive)
            throw new UnauthorizedAccessException("Refresh token has been revoked or expired");

        var userId = principial.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByEmailAsync(userId!);

    }

    private (ClaimsPrincipal principial, string jti) ValidateRefreshJwtAndGetJti(string refreshToken)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var refreshSecretKey = jwtSettings["RefreshTokenSecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var refreshExpirationMinutes = int.Parse(jwtSettings["RefreshTokenExpirationMinutes"]!);
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshSecretKey!));
        var principal = handler.ValidateToken(refreshToken, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = key,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        if (validatedToken is not JwtSecurityToken jwt)
            throw new UnauthorizedAccessException("Invalid refresh token");

        var tokenType = jwt.Claims.FirstOrDefault(c => c.Type == "token_type")?.Value;

        if(tokenType != RefreshTokenType)
    }

    public Task RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        throw new NotImplementedException();
    }

    private async Task<AuthResponseDto> GenerateTokenAsync(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var roles = await _userManager.GetRolesAsync(user);


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDto
        {
            AccessToken = tokenString,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
            Email = user.Email ?? string.Empty,
            Roles = roles
        };
    }
}
