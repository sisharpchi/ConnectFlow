using Application.Dtos;
using Application.Dtos.Auth;
using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.ServiceContracts.ServiceImplementations;

public class AuthService(IUserRepository userRepository, IRoleRepository roleRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    public async Task<LoginResponseDto> LoginAsync(LoginModel model)
    {
        User? user = (await _userRepository.GetAllUsers()).Where(user => user.UserName == model.UserName).FirstOrDefault();

        if (user is null)
        {
            throw new Exception("UserName or password incorrect");
        }

        var passwordResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, model.Password);

        if (passwordResult is PasswordVerificationResult.Failed)
            throw new Exception("UserName or password incorrect");

        var userDto = new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,   
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            UserName = model.UserName,
        };

        /*    var token = _tokenService.GenerateToken(userGetDto);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenToDB);

        var loginResponseDto = new LoginResponseDto()
        {
            AccessToken = token,
            RefreshToken = refreshToken,
            TokenType = "Bearer",
            Expires = 24
        };

         */


        return new LoginResponseDto();
    }

    public Task LogOutAsync(string token)
    {
        // We need token service and repository
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        // We need token service and repository
        throw new NotImplementedException();
    }

    public async Task<long> RegisterAsync(RegisterModel model)
    {
        var newUser = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            UserName = model.UserName,
        };

        newUser.RoleId = (await _roleRepository.GetAllRoles()).Select(role => role.Id).FirstOrDefault();
        newUser.Password = new PasswordHasher<User>().HashPassword(newUser, model.Password);
        long userId = await _userRepository.AddUser(newUser);
        return userId;
    }
}