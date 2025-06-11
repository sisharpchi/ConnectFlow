using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IRoleRepository
{
    Task<ICollection<Role>> GetAllRoles();
    Task<Role> GetRoleById(long roleId);
    Task<long> AddRole(Role role);
    Task UpdateRole(Role role);
    Task DeleteRole(Role role);

}
