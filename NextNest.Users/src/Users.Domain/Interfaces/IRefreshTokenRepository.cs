using Users.Domain.Entities;

namespace Users.Domain.Interfaces;

public interface IRefreshTokenRepository
{

  Task<RefreshToken?> GetByHashAsync(string tokenHash, CancellationToken cancellationToken = default);
  Task<IReadOnlyList<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

  Task AddAsync(RefreshToken token, CancellationToken cancellationToken = default);
  Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken = default);

  Task RevokeAllForUserAsync(Guid userId, string reason, CancellationToken cancellationToken = default);
  Task DeleteExpiredTokensAsync(CancellationToken cancellationToken = default);
}