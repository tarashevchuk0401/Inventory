namespace InventoryHub.Domain.Entities;

public class User
{
    private Guid Id { get; set; }

    private string FirstName { get; set; }

    private string LastName { get; set; }

    private string Email { get; set; }

    private string HashedPassword { get; set; }

    private string Role { get; set; }
}