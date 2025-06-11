using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    public Task<long> AddRole(Role role)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRole(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Role>> GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetRoleById(long roleId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRole(Role role)
    {
        throw new NotImplementedException();
    }
}
