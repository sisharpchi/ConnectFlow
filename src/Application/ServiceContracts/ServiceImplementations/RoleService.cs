using Application.Dtos;
using Application.RepositoryContracts;
using Domain.Entities;

namespace Application.ServiceContracts.ServiceImplementations;

public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    public async Task<long> CreateRole(CreateRoleDto createRoleDto)
    {
        var newRole = new Role
        {
            Name = createRoleDto.Name,
            Description = createRoleDto.Description,
        };

        long roleId = await _roleRepository.AddRoleAsync(newRole);
        return roleId;
    }

    public async Task<long> DeleteRole(long roleId)
    {
        Role? role = await _roleRepository.GetRoleByIdAsync(roleId);
        if (role is null)
        {
            throw new Exception("Role Not Found");
        }

        await _roleRepository.DeleteRoleAsync(role);

        return roleId;
    }

    public async Task<ICollection<RoleDto>> GetAllRoles()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        var roleDtos = new List<RoleDto>();

        foreach (var role in roles)
        {
            roleDtos.Add(new RoleDto
            {
                Name = role.Name,
                Description = role.Description,
            });

        }

        return roleDtos;
    }

    public async Task<Role> GetRoleById(long roleId)
    {
        Role? role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (role is null)
            throw new Exception("Role Not Found");

        return role;
    }

    public async Task<long> UpdateRole(long roleId, UpdateRoleDto updateRoleDto)
    {
        Role? role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (role is null)
            throw new Exception("Role not found");


        if (!string.IsNullOrEmpty(updateRoleDto.Name))
            role.Name = updateRoleDto.Name;

        if (!string.IsNullOrEmpty(updateRoleDto.Description))
            role.Name = updateRoleDto.Description;



        return role.Id;
    }
}
