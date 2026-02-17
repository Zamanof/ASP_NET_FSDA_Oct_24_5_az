using ASP_NET_18._TaskFlow_Refactoring.DTOs;

namespace ASP_NET_18._TaskFlow_Refactoring.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    Task RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
}
