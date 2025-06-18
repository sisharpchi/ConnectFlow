using Application.Dtos;
using Domain.Entities;

namespace Application.ServiceContracts;

public interface IRoleService
{
    Task<ICollection<UserRoleDto>> GetAllRolesAsync();
    Task<ICollection<UserGetDto>> GetAllUsersByRoleNameAsync(string roleName);
    Task<long> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto, string userRoleName);
    Task DeleteUserRoleByIdAsync(long userRoleId, string userRoleName);
    Task UpdateUserRoleAsync(UserRoleDto userRoleDto);
}