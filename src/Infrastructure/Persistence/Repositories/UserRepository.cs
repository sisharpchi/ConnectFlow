using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public async Task DeleteUserById(long userId)
    {
        var user = await SelectUserByIdAsync(userId);
        appDbContext.Users.Remove(user);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<long> InsertUserAsync(User user)
    {
        await appDbContext.Users.AddAsync(user);
        await appDbContext.SaveChangesAsync();
        return user.UserId;
    }

    public async Task<User> SelectUserByIdAsync(long userId)
    {
        var user = await appDbContext.Users
        .FirstOrDefaultAsync(u => u.UserId == userId); // yoki UserId bo‘lsa shuni yozing

        if (user is null)
            throw new Exception($"User with ID {userId} not found");

        return user;
    }

    public async Task<User> SelectUserByUserNameAsync(string userName)
    {
        var user = await appDbContext.Users
        .Include(u => u.Role) 
        .FirstOrDefaultAsync(u => u.UserName == userName);

        if (user is null)
            throw new Exception($"User with username '{userName}' not found");

        return user;
    }

    public async Task UpdateUserRoleAsync(long userId, long userRoleId)
    {
        var user = await SelectUserByIdAsync(userId);
        user.UserRoleId = userRoleId;
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();
    }
}
