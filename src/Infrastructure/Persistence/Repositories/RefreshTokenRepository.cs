using Application.RepositoryContracts;
using Core.Errors;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext appDbContext) : IRefreshTokenRepository
{
    public async Task InsertRefreshTokenAsync(RefreshToken refreshToken)
    {
        await appDbContext.RefreshTokens.AddAsync(refreshToken);
        await appDbContext.SaveChangesAsync();
    }

    public Task RemoveRefreshTokenAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<RefreshToken?> SelectActiveTokenByUserIdAsync(long userId)
    {
        RefreshToken? refreshToke;
        try
        {
            refreshToke = await appDbContext.RefreshTokens
            .Include(rf => rf.User)
            .SingleOrDefaultAsync(rf => rf.UserId == userId && rf.IsRevoked == false && rf.Expires > DateTime.UtcNow);
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception($"2 or more active refreshToken found with userId: {userId} found!\nAnd {ex.Message}");
        }
        return refreshToke;
    }

    public Task<RefreshToken> SelectRefreshTokenAsync(string refreshToken, long userId)
    {
        throw new NotImplementedException();
    }
}
