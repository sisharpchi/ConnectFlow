using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext appDbContext;

    public RoleRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<long> AddRoleAsync(Role role)
    {
        await appDbContext.Roles.AddAsync(role);
        await appDbContext.SaveChangesAsync();
        return role.Id;
    }

    public async Task DeleteRoleAsync(Role role)
    {
        if(role is null)
        {
            throw new Exception("not found role");
        }
        appDbContext.Roles.Remove(role);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Role>> GetAllRolesAsync()
    {
        return await appDbContext.Roles.ToListAsync();
    }

    public async Task<Role> GetRoleByIdAsync(long roleId)
    {
        var result = await appDbContext.Roles.FirstOrDefaultAsync(b => b.Id == roleId);
        if (result is null) throw new Exception("not found roleId");
        return result;
    }

    public async Task UpdateRoleAsync(Role role)
    {
        appDbContext.Roles.Update(role);
        await appDbContext.SaveChangesAsync();
    }
}
