namespace InventoryHub.Domain.Entities;

public class OrderItem
{
    public required Guid Id { get; set; }

    public required Guid OrderId { get; set; }

    public required Guid ProductId { get; set; }

    public required int Quantity { get; set; }

    public required decimal UnitPriceInOrder { get; set; }

    public static OrderItem Create(
        Guid orderId,
        Guid productId,
        int quantity,
        decimal unitPriceInOrder
    )
    {
        return new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPriceInOrder = unitPriceInOrder
        };
    }
}