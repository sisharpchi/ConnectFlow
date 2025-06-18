using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IRoleRepository
{
    Task<ICollection<Role>> SelectAllRolesAsync();
    Task<ICollection<User>> SelectAllUsersByRoleNameAsync(string roleName);
    Task<Role> SelectUserRoleByRoleName(string userRoleName);
    Task<long> InsertUserRoleAsync(Role userRole);
    Task<Role> SelectUserRoleByIdAsync(long userRoleId);
    Task DeleteUserRoleAsync(Role userRole);
    Task UpdateUserRoleAsync(Role userRole);
}
