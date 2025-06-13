using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IRoleRepository
{
    Task<ICollection<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByIdAsync(long roleId);
    Task<long> AddRoleAsync(Role role);
    Task UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(Role role);

}
