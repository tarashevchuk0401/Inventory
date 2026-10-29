using InventoryHub.Domain.Enums;

namespace InventoryHub.Application.Dto.Order;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
}
