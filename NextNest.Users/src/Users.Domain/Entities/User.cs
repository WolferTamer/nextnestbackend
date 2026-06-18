using Users.Domain.Enums;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities;

public class User
{
  private User()
  {
  }

  private User(Guid id, Email email, HashedPassword hashedPassword, UserRole role, bool isEmailVerfified, bool isActive, DateTime createdAt, DateTime editedAt)
  {
    Id = id;
    Email = email;
    PasswordHash = hashedPassword;
    Role = role;
    IsEmailVerfified = isEmailVerfified;
    IsActive = isActive;
    CreatedAt = createdAt;
    EditedAt = editedAt;
  }

  public static User Create(Guid id, string email, string hashedPassword, UserRole role, bool isEmailVerfified, bool isActive, DateTime createdAt, DateTime editedAt)
  {
    if (createdAt > DateTime.Now)
    {
      throw new ArgumentException("Creation date is in the future", nameof(createdAt));
    }
    if (editedAt > DateTime.Now)
    {
      throw new ArgumentException("Edit date is in the future", nameof(editedAt));
    }

    var emailVal = new Email(email);
    var hash = HashedPassword.FromHash(hashedPassword);

    return new User(id, emailVal, hash, role, isEmailVerfified, isActive, createdAt, editedAt);
  }

  public Guid Id { get; private set; }
  public Email Email { get; private set; }
  public HashedPassword PasswordHash { get; private set; }
  public bool IsEmailVerfified { get; private set; }
  public bool IsActive { get; private set; }
  public DateTime CreatedAt { get; private set; }

  public DateTime EditedAt { get; private set; }
  public UserRole Role { get; private set; }
}