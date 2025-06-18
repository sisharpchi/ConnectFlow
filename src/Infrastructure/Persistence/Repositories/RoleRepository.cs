using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(AppDbContext appDbContext) : IRoleRepository
{
    public Task DeleteUserRoleAsync(Role userRole)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertUserRoleAsync(Role userRole)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Role>> SelectAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> SelectAllUsersByRoleNameAsync(string roleName)
    {
        throw new NotImplementedException();
    }

    public Task<Role> SelectUserRoleByIdAsync(long userRoleId)
    {
        throw new NotImplementedException();
    }

    public Task<Role> SelectUserRoleByRoleName(string userRoleName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserRoleAsync(Role userRole)
    {
        throw new NotImplementedException();
    }
}
