using ASP_NET_12._TaskFlow_Authentication_and_Authorization.DTOs.Auth_DTOs;

namespace ASP_NET_12._TaskFlow_Authentication_and_Authorization.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequest);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequest);
}
