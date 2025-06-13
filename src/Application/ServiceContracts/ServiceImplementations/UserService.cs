
using Application.RepositoryContracts;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class UserService(IRoleRepository roleRepository) : IUserService
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    public async Task<long> ChangeRole(long roleId, string newRole)
    {
        Role? role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (role is null)
            throw new Exception("Role not found");

        role.Name = newRole;
        return role.Id;
    }
}
