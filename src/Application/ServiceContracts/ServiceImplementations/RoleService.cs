using Application.Dtos;
using Application.RepositoryContracts;
using Core.Errors;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class RoleService(IRoleRepository userRoleRepository) : IRoleService
{
    public async Task<long> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto, string userRoleName)
    {
        if (userRoleName != "SuperAdmin")
            throw new NotAllowedException("Access only allowed to SuperAdmin to add user roles");

        var userRole = new Role()
        {
            Name = userRoleCreateDto.UserRoleName,
            Description = userRoleCreateDto.Description,
        };

        var userRoleId = await userRoleRepository.InsertUserRoleAsync(userRole);

        return userRoleId;

    }

    public async Task DeleteUserRoleByIdAsync(long userRoleId, string userRoleName)
    {
        if (userRoleName != "SuperAdmin")
            throw new NotAllowedException("Access denied for non SuperAdmins");

        var userRole = await userRoleRepository.SelectUserRoleByIdAsync(userRoleId);
        await userRoleRepository.DeleteUserRoleAsync(userRole);
    }

    public async Task<ICollection<UserRoleDto>> GetAllRolesAsync()
    {
        var userRoels = await userRoleRepository.SelectAllRolesAsync();

        var userRoleDto = userRoels.Select(userRole => ConverUserRoleToUserRoleDto(userRole)).ToList();
        return userRoleDto;
    }

    public async Task<ICollection<UserGetDto>> GetAllUsersByRoleNameAsync(string roleName)
    {
        var users = await userRoleRepository.SelectAllUsersByRoleNameAsync(roleName);

        var userRolesDto = users.Select(user => ConvertUserToUserGetDto(user)).ToList();
        return userRolesDto;
    }

    public async Task UpdateUserRoleAsync(UserRoleDto userRoleDto)
    {
        var userRole = await userRoleRepository.SelectUserRoleByIdAsync(userRoleDto.UserRoleId);
        if (userRole is null) throw new NotFoundException($"User role with roleId: {userRoleDto.UserRoleId} not found");
        else
        {
            userRole.Name = userRoleDto.UserRoleName;
            userRole.Description = userRoleDto.Description;

            await userRoleRepository.UpdateUserRoleAsync(userRole);
        }
    }
    private UserRoleDto ConverUserRoleToUserRoleDto(Role role)
    {
        return new UserRoleDto()
        {
            UserRoleId = role.RoleId,
            UserRoleName = role.Name,
            Description = role.Description,
        };
    }
    private UserGetDto ConvertUserToUserGetDto(User user)
    {
        return new UserGetDto()
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name,
        };
    }
}
