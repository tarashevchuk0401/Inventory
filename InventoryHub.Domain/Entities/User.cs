namespace InventoryHub.Domain.Entities;

public class User
{
    public required Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string HashedPassword { get; set; }

    public required string Role { get; set; }

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