using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using InventoryHub.Domain.Enums;

namespace InventoryHub.Application.Dto.Product;

public class UpdateProductDto
{
    [Required] public Guid Id { get; set; }

    [Required, MinLength(1)] public required string Name { get; set; }

    [Range(0.01, double.MaxValue)] public required decimal Price { get; set; }

    [Range(0, int.MaxValue)] public required int StockQuantity { get; set; }

    [Required] public required Guid SupplierId { get; set; }

    [Required] public required ProductCategories Category { get; set; }
}
