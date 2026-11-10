using InventoryHub.Application.Dto.OrderItem;
using InventoryHub.Domain.Entities;
using InventoryHub.Domain.Enums;

public class OrderSummaryDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> OrderItems { get; set;  }
    public decimal TotalSum { get; set; }
    public int ItemsCount { get; set; }
}
