using Users.Domain.Constants;
using Users.Domain.Interfaces;

namespace Users.Domain.Entities;

public sealed class RefreshToken
{
  public Guid Id { get; private set; }
  public Guid UserId { get; private set; }

  // Stored as a hash — raw token is only held in memory at generation time
  public string TokenHash { get; private set; }

  public DateTime CreatedAt { get; private set; }
  public DateTime ExpiresAt { get; private set; }

  public bool IsRevoked { get; private set; }
  public DateTime? RevokedAt { get; private set; }
  public string? RevokedReason { get; private set; }

  // Supports token rotation — tracks which token replaced this one
  public string? ReplacedByTokenHash { get; private set; }

  // Metadata for session tracking / suspicious activity detection
  public string? DeviceInfo { get; private set; }
  public string? IpAddress { get; private set; }

  // Computed — a token is only valid if unexpired AND not revoked
  public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAt;

  // Required by EF Core — never call directly
  private RefreshToken() { }

  private RefreshToken(
      Guid userId,
      string tokenHash,
      DateTime expiresAt,
      string? deviceInfo,
      string? ipAddress)
  {
    Id = Guid.NewGuid();
    UserId = userId;
    TokenHash = tokenHash;
    CreatedAt = DateTime.UtcNow;
    ExpiresAt = expiresAt;
    IsRevoked = false;
    DeviceInfo = deviceInfo;
    IpAddress = ipAddress;
  }

  public static RefreshToken Create(
      Guid userId,
      string tokenHash,
      DateTime expiresAt,
      string? deviceInfo = null,
      string? ipAddress = null)
  {
    if (userId == Guid.Empty)
      throw new ArgumentException("UserId cannot be empty.", nameof(userId));

    if (string.IsNullOrWhiteSpace(tokenHash))
      throw new ArgumentException("Token hash cannot be empty.", nameof(tokenHash));

    if (expiresAt <= DateTime.UtcNow)
      throw new ArgumentException("Expiry must be in the future.", nameof(expiresAt));

    return new RefreshToken(userId, tokenHash, expiresAt, deviceInfo, ipAddress);
  }

  /// <summary>
  /// Revokes this token for a given reason.
  /// Called on logout, password change, or suspicious activity detection.
  /// </summary>
  public void Revoke(string reason)
  {
    if (IsRevoked)
      throw new InvalidOperationException("Token is already revoked.");

    IsRevoked = true;
    RevokedAt = DateTime.UtcNow;
    RevokedReason = reason;
  }

  /// <summary>
  /// Revokes this token and records which token replaced it.
  /// Called during normal token rotation on refresh.
  /// </summary>
  public void RotateTo(string replacementTokenHash)
  {
    if (string.IsNullOrWhiteSpace(replacementTokenHash))
      throw new ArgumentException("Replacement token hash cannot be empty.", nameof(replacementTokenHash));

    Revoke(RevokeReasons.Rotated);
    ReplacedByTokenHash = replacementTokenHash;
  }

  /// <summary>
  /// Checks if a presented plaintext token matches this stored token hash.
  /// Depends on the same hashing approach used at creation time.
  /// </summary>
  public bool MatchesToken(string plaintextToken, ITokenHasher tokenHasher)
      => tokenHasher.Verify(plaintextToken, TokenHash);
}