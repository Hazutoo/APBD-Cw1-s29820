using APBD_Cw1_s29820.Domain.Enums;

namespace APBD_Cw1_s29820.Domain.Users;

public abstract class User
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public UserType UserType { get; }
    public string FullName => $"{FirstName} {LastName}";

    protected User(string firstName, string lastName, UserType userType)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }

        Id = _nextId++;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        UserType = userType;
    }

    public void ChangeFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        }

        FirstName = firstName.Trim();
    }

    public void ChangeLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }

        LastName = lastName.Trim();
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} ({UserType})";
    }
}
