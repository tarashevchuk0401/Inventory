namespace InventoryHub.Application.Dto.OrderItem;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPriceInOrder { get; set; }
}
