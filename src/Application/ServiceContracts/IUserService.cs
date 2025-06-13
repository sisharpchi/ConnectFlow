namespace Application.ServiceContracts;

public interface IUserService
{
    Task<long> ChangeRole(long roleId, string newRole);
}
