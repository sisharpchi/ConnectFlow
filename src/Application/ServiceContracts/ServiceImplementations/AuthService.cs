using Application.Dtos;
using Application.Helpers;
using Application.RepositoryContracts;

namespace Application.ServiceContracts.ServiceImplementations;

public class AuthService(IUserRepository userRepositroy, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IRoleRepository userRoleRepository) : IAuthService
{
    public Task<LogInResponseDto> LoginUserAsync(UserLogInDto userLogInDto)
    {
        throw new NotImplementedException();
    }

    public Task LogOutAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<long> SignUpUserAsync(UserCreateDto userCreateDto)
    {
        throw new NotImplementedException();
    }
}