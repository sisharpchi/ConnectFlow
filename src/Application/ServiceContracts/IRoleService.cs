using Application.Dtos;
using Domain.Entities;

namespace Application.ServiceContracts;

public interface IRoleService
{
    Task<ICollection<RoleDto>> GetAllRoles();
    Task<Role> GetRoleById(long roleId);
    Task<long> CreateRole(CreateRoleDto createRoleDto);
    Task<long> UpdateRole(long roleId,UpdateRoleDto updateRoleDto);
    Task<long> DeleteRole(long roleId);
}