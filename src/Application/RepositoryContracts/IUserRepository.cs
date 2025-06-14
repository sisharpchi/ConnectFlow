using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IUserRepository
{
    Task<IQueryable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(long userId);
    Task<long> AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    
}