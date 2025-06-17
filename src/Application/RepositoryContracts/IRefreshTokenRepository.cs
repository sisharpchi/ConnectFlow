using Domain.Entities;

namespace Application.RepositoryContracts;

public interface IRefreshTokenRepository
{
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> SelectRefreshTokenAsync(string refreshToken, long userId);
    Task RemoveRefreshTokenAsync(string token);
}
