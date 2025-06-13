using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task<long> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
