using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<long> AddUserAsync(User user)
    {
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task DeleteUserAsync(User user)
    {
        if (user is null)
        {
            throw new Exception("not found user");
        }

        appDbContext.Users.Remove(user);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await appDbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(long userId)
    {
       var result = await appDbContext.Users.FirstOrDefaultAsync(b => b.Id == userId);
       if(result is null)
        {
            throw new Exception("not found userId");
        }
        return result;
    }

    public async Task UpdateUserAsync(User user)
    {
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();
    }
}
