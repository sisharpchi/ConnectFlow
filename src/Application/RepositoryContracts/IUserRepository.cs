using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IUserRepository
{
    Task<ICollection<User>> GetAllRoles();
    Task<User> GetRoleById(long userId);
    Task<long> AddRole(User user);
    Task UpdateRole(User user);
    Task DeleteRole(User user);
}
