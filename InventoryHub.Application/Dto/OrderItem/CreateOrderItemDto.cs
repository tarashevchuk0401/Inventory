using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto.OrderItem;

public class CreateOrderItemDto
{
    [Required] public Guid OrderId { get; set; }

    [Required] public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)] public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)] public decimal UnitPriceInOrder { get; set; }
}
