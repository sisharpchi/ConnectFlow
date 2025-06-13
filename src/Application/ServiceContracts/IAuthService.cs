using Application.Dtos;
using Application.Dtos.Auth;

namespace Application.ServiceContracts;

public interface IAuthService
{
    Task<long> RegisterAsync(RegisterModel model);
    Task<LoginResponseDto> LoginAsync(LoginModel model);
    Task LogOutAsync(string token);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request);
}