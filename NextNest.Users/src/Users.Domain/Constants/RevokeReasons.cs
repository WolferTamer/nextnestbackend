namespace Users.Domain.Constants;

public static class RevokeReasons
{
  public const string Rotated = "Replaced by new token during refresh.";
  public const string UserLoggedOut = "User initiated logout.";
  public const string LoggedOutAllDevices = "User logged out of all devices.";
  public const string PasswordChanged = "Invalidated due to password change.";
  public const string SuspiciousActivity = "Revoked due to suspected token reuse.";
  public const string AdminRevoked = "Revoked by administrator.";
}