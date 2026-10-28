namespace InventoryHub.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int  Quantity { get; set; }

    public decimal UnitPriceInOrder { get; set; }

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
