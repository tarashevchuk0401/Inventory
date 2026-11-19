using System.ComponentModel.DataAnnotations;
using InventoryHub.Domain.Enums;

public class UpdateOrderStatusDto
{
    [Required] public required OrderStatus Status { get; set; }
}