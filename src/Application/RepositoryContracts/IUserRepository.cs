using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IUserRepository
{
    Task<ICollection<User>> GetAllUsers();
    Task<User> GetUserById(long userId);
    Task<long> AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}
