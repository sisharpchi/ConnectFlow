using Application.Dtos;
using Application.RepositoryContracts;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class RoleService(IRoleRepository userRoleRepository) : IRoleService
{
    public Task<long> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto, string userRoleName)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserRoleByIdAsync(long userRoleId, string userRoleName)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserRoleDto>> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<UserGetDto>> GetAllUsersByRoleNameAsync(string roleName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserRoleAsync(UserRoleDto userRoleDto)
    {
        throw new NotImplementedException();
    }
}
