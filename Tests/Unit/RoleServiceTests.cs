using Xunit;
using Moq;
using System.Threading.Tasks;
using Application.ServiceContracts.ServiceImplementations;
using Application.RepositoryContracts;
using Application.Dtos;
using Core.Errors;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

public class RoleServiceTests
{
    private readonly Mock<IRoleRepository> _roleRepositoryMock = new();
    private readonly RoleService _roleService;

    public RoleServiceTests()
    {
        _roleService = new RoleService(_roleRepositoryMock.Object);
    }

    [Fact]
    public async Task AddUserRoleAsync_ShouldAddRole_WhenUserIsSuperAdmin()
    {
        // Arrange
        var dto = new UserRoleCreateDto
        {
            UserRoleName = "Moderator",
            Description = "Moderators group"
        };

        _roleRepositoryMock.Setup(r => r.InsertUserRoleAsync(It.IsAny<Role>()))
            .ReturnsAsync(1);

        // Act
        var result = await _roleService.AddUserRoleAsync(dto, "SuperAdmin");

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task AddUserRoleAsync_ShouldThrowNotAllowed_WhenUserIsNotSuperAdmin()
    {
        // Arrange
        var dto = new UserRoleCreateDto
        {
            UserRoleName = "Admin",
            Description = "Admins"
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotAllowedException>(() =>
            _roleService.AddUserRoleAsync(dto, "User"));
    }

    [Fact]
    public async Task DeleteUserRoleByIdAsync_ShouldDelete_WhenUserIsSuperAdmin()
    {
        // Arrange
        var role = new Role { RoleId = 10, Name = "OldRole" };

        _roleRepositoryMock.Setup(r => r.SelectUserRoleByIdAsync(10))
            .ReturnsAsync(role);

        _roleRepositoryMock.Setup(r => r.DeleteUserRoleAsync(role))
            .Returns(Task.CompletedTask);

        // Act
        await _roleService.DeleteUserRoleByIdAsync(10, "SuperAdmin");

        // Assert
        _roleRepositoryMock.Verify(r => r.DeleteUserRoleAsync(role), Times.Once);
    }

    [Fact]
    public async Task DeleteUserRoleByIdAsync_ShouldThrowNotAllowed_WhenUserIsNotSuperAdmin()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotAllowedException>(() =>
            _roleService.DeleteUserRoleByIdAsync(1, "User"));
    }

    [Fact]
    public async Task GetAllRolesAsync_ShouldReturnMappedDtos()
    {
        // Arrange
        var roles = new List<Role>
        {
            new Role { RoleId = 1, Name = "Admin", Description = "All access" },
            new Role { RoleId = 2, Name = "User", Description = "Basic access" }
        };

        _roleRepositoryMock.Setup(r => r.SelectAllRolesAsync())
            .ReturnsAsync(roles);

        // Act
        var result = await _roleService.GetAllRolesAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.UserRoleName == "Admin");
    }

    [Fact]
    public async Task GetAllUsersByRoleNameAsync_ShouldReturnMappedUserDtos()
    {
        // Arrange
        var users = new List<User>
        {
            new User {
                UserId = 1,
                FirstName = "Ali",
                LastName = "Aliyev",
                UserName = "ali",
                Email = "ali@example.com",
                PhoneNumber = "998901234567",
                Role = new Role { Name = "Admin" }
            }
        };

        _roleRepositoryMock.Setup(r => r.SelectAllUsersByRoleNameAsync("Admin"))
            .ReturnsAsync(users);

        // Act
        var result = await _roleService.GetAllUsersByRoleNameAsync("Admin");

        // Assert
        Assert.Single(result);
        Assert.Equal("Ali", result.First().FirstName);
        Assert.Equal("Admin", result.First().Role);
    }

    [Fact]
    public async Task UpdateUserRoleAsync_ShouldUpdate_WhenRoleExists()
    {
        // Arrange
        var dto = new UserRoleDto
        {
            UserRoleId = 1,
            UserRoleName = "Editor",
            Description = "Can edit content"
        };

        var role = new Role
        {
            RoleId = 1,
            Name = "Old",
            Description = "Old Desc"
        };

        _roleRepositoryMock.Setup(r => r.SelectUserRoleByIdAsync(dto.UserRoleId))
            .ReturnsAsync(role);

        _roleRepositoryMock.Setup(r => r.UpdateUserRoleAsync(It.IsAny<Role>()))
            .Returns(Task.CompletedTask);

        // Act
        await _roleService.UpdateUserRoleAsync(dto);

        // Assert
        _roleRepositoryMock.Verify(r => r.UpdateUserRoleAsync(It.Is<Role>(r => r.Name == "Editor")), Times.Once);
    }

    [Fact]
    public async Task UpdateUserRoleAsync_ShouldThrowNotFound_WhenRoleNotExists()
    {
        // Arrange
        var dto = new UserRoleDto { UserRoleId = 99 };

        _roleRepositoryMock.Setup(r => r.SelectUserRoleByIdAsync(dto.UserRoleId))
            .ReturnsAsync((Role)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _roleService.UpdateUserRoleAsync(dto));
    }
}
