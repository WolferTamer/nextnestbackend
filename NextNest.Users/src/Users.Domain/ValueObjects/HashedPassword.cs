namespace Users.Domain.ValueObjects;

public class HashedPassword : ValueObject
{
  public string Value { get; }

  private HashedPassword(string hashedValue)
  {
    Value = hashedValue;
  }

  public static HashedPassword FromHash(string hashedValue)
  {
    if (string.IsNullOrWhiteSpace(hashedValue)) throw new ArgumentException("Hashed password cannot be empty.", nameof(hashedValue));

    if (hashedValue.Length < 20) throw new ArgumentException("Value does not appear to be a valid hash", nameof(hashedValue));

    return new HashedPassword(hashedValue);
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
  public override string ToString() => "[PROTECTED]";
}