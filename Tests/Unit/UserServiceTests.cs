using Xunit;
using Moq;
using System.Threading.Tasks;
using Application.ServiceContracts.ServiceImplementations;
using Application.RepositoryContracts;
using Core.Errors;
using Domain.Entities;
using Application.Dtos;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task DeleteUserByUserIdAsync_ShouldDelete_WhenSuperAdmin()
    {
        // Arrange
        long userId = 1;

        _userRepositoryMock.Setup(r => r.DeleteUserById(userId)).Returns(Task.CompletedTask);

        // Act
        await _userService.DeleteUserByUserIdAsync(userId, "SuperAdmin");

        // Assert
        _userRepositoryMock.Verify(r => r.DeleteUserById(userId), Times.Once);
    }

    [Fact]
    public async Task DeleteUserByUserIdAsync_ShouldDelete_WhenAdminDeletingUser()
    {
        // Arrange
        long userId = 2;
        var user = new User
        {
            UserId = userId,
            Role = new Role { Name = "User" }
        };

        _userRepositoryMock.Setup(r => r.SelectUserByIdAsync(userId)).ReturnsAsync(user);
        _userRepositoryMock.Setup(r => r.DeleteUserById(userId)).Returns(Task.CompletedTask);

        // Act
        await _userService.DeleteUserByUserIdAsync(userId, "Admin");

        // Assert
        _userRepositoryMock.Verify(r => r.DeleteUserById(userId), Times.Once);
    }

    [Fact]
    public async Task DeleteUserByUserIdAsync_ShouldThrow_WhenAdminTryingToDeleteAdmin()
    {
        // Arrange
        long userId = 3;
        var user = new User
        {
            UserId = userId,
            Role = new Role { Name = "Admin" }
        };

        _userRepositoryMock.Setup(r => r.SelectUserByIdAsync(userId)).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<NotAllowedException>(() =>
            _userService.DeleteUserByUserIdAsync(userId, "Admin"));
    }

    [Fact]
    public async Task DeleteUserByUserIdAsync_ShouldThrowForbidden_WhenUserTriesToDelete()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ForbiddenException>(() =>
            _userService.DeleteUserByUserIdAsync(1, "User"));
    }

    [Fact]
    public async Task GetUserByUserIdAsync_ShouldReturnUserGetDto()
    {
        // Arrange
        long userId = 5;
        var user = new User
        {
            UserId = userId,
            FirstName = "Ali",
            LastName = "Aliyev",
            UserName = "ali",
            Email = "ali@example.com",
            PhoneNumber = "998901234567",
            Role = new Role { Name = "User" }
        };

        _userRepositoryMock.Setup(r => r.SelectUserByIdAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _userService.GetUserByUserIdAsync(userId);

        // Assert
        Assert.Equal("Ali", result.FirstName);
        Assert.Equal("User", result.Role);
    }

    [Fact]
    public async Task UpdateUserRoleAsync_ShouldUpdate_WhenSuperAdmin()
    {
        // Arrange
        long userId = 1;
        long roleId = 2;

        _userRepositoryMock.Setup(r => r.UpdateUserRoleAsync(userId, roleId))
            .Returns(Task.CompletedTask);

        // Act
        await _userService.UpdateUserRoleAsync(userId, roleId, "SuperAdmin");

        // Assert
        _userRepositoryMock.Verify(r => r.UpdateUserRoleAsync(userId, roleId), Times.Once);
    }

    [Fact]
    public async Task UpdateUserRoleAsync_ShouldThrow_WhenNotSuperAdmin()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotAllowedException>(() =>
            _userService.UpdateUserRoleAsync(1, 2, "User"));
    }
}
