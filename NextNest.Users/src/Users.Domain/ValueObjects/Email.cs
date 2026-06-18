using System.Text.RegularExpressions;

namespace Users.Domain.ValueObjects;

public class Email : ValueObject
{
  public string Name { get; private set; }
  public string Domain { get; private set; }

  public Email(string address)
  {
    var parts = address.Split('@');
    if (parts.Length != 2)
    {
      throw new ArgumentException("The email was not properly seperated by an @ symbol.");
    }

    string namePattern = @"^[a-zA-Z0-9._%+-]+";
    var nameValid = Regex.IsMatch(parts[0], namePattern);
    if (!nameValid)
    {
      throw new ArgumentException("The username of the address contains invalid characters.");
    }

    string domainPattern = @"[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$/.";
    var domainValid = Regex.IsMatch(parts[1], domainPattern);
    if (!domainValid)
    {
      throw new ArgumentException("The domain of the address is invalid.");
    }

    Name = parts[0];
    Domain = parts[1];

  }

  public Email(string name, string domain)
  {

    string namePattern = @"^[a-zA-Z0-9._%+-]+";
    var nameValid = Regex.IsMatch(name, namePattern);
    if (!nameValid)
    {
      throw new ArgumentException("The username of the address contains invalid characters.");
    }

    string domainPattern = @"[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$/.";
    var domainValid = Regex.IsMatch(domain, domainPattern);
    if (!domainValid)
    {
      throw new ArgumentException("The domain of the address is invalid.");
    }

    Name = name;
    Domain = domain;
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return Name;
    yield return Domain;
  }

  public override string ToString()
  {
    return Name + '@' + Domain;
  }
}