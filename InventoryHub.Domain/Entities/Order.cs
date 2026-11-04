using InventoryHub.Domain.Enums;

namespace InventoryHub.Domain.Entities;

public class Order
{
    public required Guid Id { get; set; }

    public required Guid CustomerId { get; set; }

    public required DateTime OrderDate { get; set; }

    public required OrderStatus Status { get; set; }

    public required List<OrderItem> OrderItems { get; set; } = new();

    public static Order Create(Guid customerId)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            OrderItems = new List<OrderItem>()
        };
    }
}