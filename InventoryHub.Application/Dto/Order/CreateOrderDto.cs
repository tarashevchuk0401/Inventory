using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto.Order;

public class CreateOrderDto
{
    [Required] public Guid CustomerId { get; set; }
}
