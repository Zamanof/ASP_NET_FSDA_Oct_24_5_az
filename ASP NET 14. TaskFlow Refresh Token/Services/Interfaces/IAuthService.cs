using ASP_NET_14._TaskFlow_Refresh_Token.DTOs.Auth_DTOs;

namespace ASP_NET_14._TaskFlow_Refresh_Token.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    Task RevokeRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
}
