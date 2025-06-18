using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public Task DeleteUserById(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<long> InsertUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> SelectUserByIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> SelectUserByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserRoleAsync(long userId, long userRoleId)
    {
        throw new NotImplementedException();
    }
}
