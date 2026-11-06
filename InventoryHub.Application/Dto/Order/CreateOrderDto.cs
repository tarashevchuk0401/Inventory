using System.ComponentModel.DataAnnotations;
using InventoryHub.Application.Dto.OrderItem;

namespace InventoryHub.Application.Dto.Order;

public class CreateOrderDto
{
    [Required] public Guid CustomerId { get; set; }

    [Required]
    [MinLength(1)]
    public List<CreateOrderItemDto> OrderItems { get; set; }

}
