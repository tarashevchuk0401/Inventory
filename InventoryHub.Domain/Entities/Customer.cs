namespace InventoryHub.Domain.Entities;

public class Customer
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

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
