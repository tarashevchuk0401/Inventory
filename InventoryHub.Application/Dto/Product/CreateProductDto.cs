using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto.Product;

public class CreateProductDto
{
    [Required, MinLength(1)] public string Name { get; set; }

    [Range(0.01, double.MaxValue)] public decimal Price { get; set; }

    [Range(0, int.MaxValue)] public int StockQuantity { get; set; }

    [Required] public Guid SupplierId { get; set; }

    [Required] public Guid CategoryId { get; set; }
}
