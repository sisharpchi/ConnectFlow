using Application.Dtos;
using Application.FluintValidation;
using Application.Helpers;
using Application.Helpers.PasswordHasher;
using Application.RepositoryContracts;
using Core.Core.Errors;
using Core.Errors;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class AuthService(IUserRepository userRepositroy, ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IRoleRepository userRoleRepository) : IAuthService
{
    public async Task<LogInResponseDto> LoginUserAsync(UserLogInDto userLogInDto)
    {
        var logInValidator = new UserLogInDtoValidator();
        var result = logInValidator.Validate(userLogInDto);

        if (!result.IsValid)
        {
            var errors = "";
            foreach (var error in result.Errors)
            {
                errors = errors + "\n" + error.ErrorMessage;
            }
            throw new ValidateFailedException(errors);
        }

        var user = await userRepositroy.SelectUserByUserNameAsync(userLogInDto.UserName);

        var checkUserPassword = PasswordHasher.Verify(userLogInDto.Password, user.Password, user.Salt);
        if (checkUserPassword == false)
        {
            throw new UnauthorizedException("User or password incorrect");
        }

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name,
        };

        var token = tokenService.GenerateTokent(userGetDto);
        var existingToken = await refreshTokenRepository.SelectActiveTokenByUserIdAsync(user.UserId);

        var loginResponseDto = new LogInResponseDto()
        {
            AccessToken = token,
            TokenType = "Bearer",
            Expires = 24,
        };

        if (existingToken == null)
        {
            var refreshToken = tokenService.GenerateRefreshToken();
            var refreshTokenToDB = new RefreshToken()
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(21),
                IsRevoked = false,
                UserId = user.UserId,
            };

            await refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

            loginResponseDto.RefreshToken = refreshToken;
        }
        else
        {
            loginResponseDto.RefreshToken = existingToken.Token;
        }

        return loginResponseDto;
    }

    public async Task LogOutAsync(string token)
    {
        await refreshTokenRepository.RemoveRefreshTokenAsync(token);
    }

    public async Task<LogInResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null) throw new ForbiddenException("Invalid Access token");

        var userClaim = principal.FindFirst(c => c.Type == "UserId");
        var userId = long.Parse(userClaim.Value);

        var refreshToken = await refreshTokenRepository.SelectRefreshTokenAsync(request.RefreshToken, userId);
        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow || refreshToken.IsRevoked)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        // make refresh token used
        refreshToken.IsRevoked = true;

        var user = await userRepositroy.SelectUserByIdAsync(userId);

        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name,
        };

        // issue new tokens
        var newAccessToken = tokenService.GenerateTokent(userGetDto);
        var newRefreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = newRefreshToken,
            Expires = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId,
        };

        await refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LogInResponseDto()
        {
            AccessToken = newAccessToken,
            TokenType = "Bearer",
            RefreshToken = newRefreshToken,
            Expires = 900,
        };
    }

    public async Task<long> SignUpUserAsync(UserCreateDto userCreateDto)
    {
        var userValidator = new UserCreateDtoValidator();
        var result = userValidator.Validate(userCreateDto);


        if (!result.IsValid)
        {
            var errors = "";
            foreach (var error in result.Errors)
            {
                errors = errors + error.ErrorMessage;
            }
            throw new ValidateFailedException(errors);
        }

        var tupleFromHasher = PasswordHasher.Hasher(userCreateDto.Password);

        var userRoleName = "User";
        var userRoleOfUser = await userRoleRepository.SelectUserRoleByRoleName(userRoleName);

        var user = new User()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PhoneNumber = userCreateDto.PhoneNumber,
            Password = tupleFromHasher.Hash,
            Salt = tupleFromHasher.Salt,
            UserRoleId = userRoleOfUser.RoleId,
        };

        return await userRepositroy.InsertUserAsync(user);
    }
}
