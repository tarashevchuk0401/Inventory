namespace InventoryHub.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    // ???
    public string Status { get; set; }
    public string OrderItems { get; set; }
}
