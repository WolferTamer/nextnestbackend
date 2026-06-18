using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Domain.Interfaces;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
  Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
  Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);

  Task AddAsync(User user, CancellationToken cancellationToken = default);
  Task UpdateAsync(User user, CancellationToken cancellationToken = default);
  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}