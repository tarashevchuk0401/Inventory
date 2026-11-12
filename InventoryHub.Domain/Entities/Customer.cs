namespace InventoryHub.Domain.Entities;

public class Customer
{
    public required Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();

    public static Customer Create(
        string firstName,
        string lastName,
        string email
    )
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }
}
