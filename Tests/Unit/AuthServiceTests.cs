using Application.Dtos;
using Application.Helpers;
using Application.Helpers.PasswordHasher;
using Application.RepositoryContracts;
using Application.ServiceContracts.ServiceImplementations;
using Core.Core.Errors;
using Core.Errors;
using Domain.Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock = new();
    private readonly Mock<IRoleRepository> _roleRepositoryMock = new();

    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _authService = new AuthService(
            _userRepositoryMock.Object,
            _tokenServiceMock.Object,
            _refreshTokenRepositoryMock.Object,
            _roleRepositoryMock.Object
        );
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new UserLogInDto
        {
            UserName = "testuser",
            Password = "password123"
        };

        var user = new User
        {
            UserId = 1,
            UserName = "testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            PhoneNumber = "1234567890",
            Password = "hashedPassword",
            Salt = "salt",
            Role = new Role { Name = "User" }
        };

        _userRepositoryMock.Setup(r => r.SelectUserByUserNameAsync(loginDto.UserName))
            .ReturnsAsync(user);

        // passwordni to'g'ri deb qabul qilamiz
        //PasswordHasher.MockVerifyResult = true;

        _tokenServiceMock.Setup(t => t.GenerateTokent(It.IsAny<UserGetDto>()))
            .Returns("access_token");

        _refreshTokenRepositoryMock.Setup(r => r.SelectActiveTokenByUserIdAsync(user.UserId))
            .ReturnsAsync((RefreshToken)null);

        _tokenServiceMock.Setup(t => t.GenerateRefreshToken())
            .Returns("refresh_token");

        _refreshTokenRepositoryMock.Setup(r => r.InsertRefreshTokenAsync(It.IsAny<RefreshToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _authService.LoginUserAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("access_token", result.AccessToken);
        Assert.Equal("refresh_token", result.RefreshToken);
        Assert.Equal("Bearer", result.TokenType);
    }

    [Fact]
    public async Task LoginUserAsync_ShouldThrow_WhenPasswordIsIncorrect()
    {
        // Arrange
        var loginDto = new UserLogInDto
        {
            UserName = "wronguser",
            Password = "wrongpassword"
        };

        var user = new User
        {
            UserId = 1,
            UserName = "wronguser",
            Password = "hashedPassword",
            Salt = "salt",
            Role = new Role { Name = "User" }
        };

        _userRepositoryMock.Setup(r => r.SelectUserByUserNameAsync(loginDto.UserName))
            .ReturnsAsync(user);

        //PasswordHasher.MockVerifyResult = false; // noto‘g‘ri parol

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedException>(() =>
            _authService.LoginUserAsync(loginDto));
    }

    [Fact]
    public async Task LoginUserAsync_ShouldThrowValidationException_WhenUserNameIsMissing()
    {
        // Arrange
        var loginDto = new UserLogInDto
        {
            UserName = "", // invalid
            Password = "somepassword"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidateFailedException>(() =>
            _authService.LoginUserAsync(loginDto));
    }
}
