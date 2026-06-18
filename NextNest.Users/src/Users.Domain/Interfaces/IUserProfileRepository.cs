using Users.Domain.Entities;

namespace Users.Domain.Interfaces;

public interface IUserProfileRepository
{
  Task<UserProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
  Task AddAsync(UserProfile profile, CancellationToken cancellationToken = default);
  Task UpdateAsync(UserProfile profile, CancellationToken cancellationToken = default);
}