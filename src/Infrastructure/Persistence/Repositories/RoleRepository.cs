using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(AppDbContext appDbContext) : IRoleRepository
{
    public async Task<long> InsertUserRoleAsync(Role role)
    {
        await appDbContext.UserRoles.AddAsync(role);
        await appDbContext.SaveChangesAsync();
        return role.RoleId;
    }

    public async Task<ICollection<Role>> SelectAllRolesAsync()
    {
        var userRoles = await appDbContext.UserRoles.ToListAsync();
        return userRoles;
    }

    public async Task<ICollection<User>> SelectAllUsersByRoleNameAsync(string roleName)
    {
        var users = await appDbContext.Users.Include(u => u.Role).Where(u => u.Role.Name == roleName).ToListAsync();
        return users;
    }

    public async Task<Role> SelectUserRoleByRoleName(string userRoleName)
    {
        var userRole = await appDbContext.UserRoles.FirstOrDefaultAsync(uR => uR.Name == userRoleName);
        return userRole == null ? throw new Exception($"Role with role name: {userRoleName} not found") : userRole;
    }

    public async Task<Role> SelectUserRoleByIdAsync(long userRoleId)
    {
        var userRole = await appDbContext.UserRoles.FirstOrDefaultAsync(ur => ur.RoleId == userRoleId);
        return userRole ?? throw new Exception($"User role with: {userRoleId} not found");
    }
    public async Task DeleteUserRoleAsync(Role role)
    {
        appDbContext.UserRoles.Remove(role);
        await appDbContext.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(Role role)
    {
        appDbContext.UserRoles.Update(role);
        await appDbContext.SaveChangesAsync();
    }
}
