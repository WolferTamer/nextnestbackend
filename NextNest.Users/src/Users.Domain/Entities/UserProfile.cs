using System.Dynamic;

namespace Users.Domain.Entities;

public class UserProfile
{

  private UserProfile()
  {

  }

  private UserProfile(Guid id, Guid userId, string displayName)
  {
    Id = id;
    UserId = userId;
    DisplayName = displayName;
  }

  public static UserProfile Create(Guid id, Guid userId, string displayName)
  {
    if (displayName.Length > 25 || displayName.Length < 3) throw new ArgumentException("Display name must be between 3 and 25 characters.", nameof(displayName));
    return new UserProfile(id, userId, displayName);
  }

  public Guid Id { get; private set; }
  public Guid UserId { get; private set; }
  public string DisplayName { get; private set; }
}