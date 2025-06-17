using Application.RepositoryContracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext appDbContext) : IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _appDbContext.RefreshTokens.AddAsync(refreshToken);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenAsync(string token)
    {
        var rToken = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

        if (rToken is null) throw new Exception($"Refresh token {token} not found");

        _appDbContext.RefreshTokens.Remove(rToken);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken?> SelectRefreshTokenAsync(string refreshToken, long userId)
        => await _appDbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == refreshToken && rt.UserId == userId);
}
