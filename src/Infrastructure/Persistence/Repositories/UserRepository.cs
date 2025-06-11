using Application.RepositoryContracts;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public Task<long> AddRole(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRole(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<User>> GetAllRoles()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetRoleById(long userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRole(User user)
    {
        throw new NotImplementedException();
    }
}
