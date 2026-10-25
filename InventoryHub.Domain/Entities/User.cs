namespace InventoryHub.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string HashedPassword { get; set; }

    public string Role { get; set; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash
    )
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email,
            HashedPassword = passwordHash,
            Role = "user"
        };
    }

    public static User Create(
        string id,
        string firstName,
        string lastName,
        string email,
        string passwordHash
    )
    {
        return new User
        {
            Id = Guid.Parse(id),
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email,
            HashedPassword = passwordHash,
            Role = "user"
        };
    }
}
