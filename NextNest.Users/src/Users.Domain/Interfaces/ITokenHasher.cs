namespace Users.Domain.Interfaces;

public interface ITokenHasher
{
  string Hash(string plaintextToken);
  bool Verify(string plaintextToken, string tokenHash);
}