namespace InventoryHub.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public static Supplier Create(
        string email,
        string name
    )
    {
        return new Supplier
        {
            Id = Guid.NewGuid(),
            Email = email,
            Name = name
        };
    }
}
