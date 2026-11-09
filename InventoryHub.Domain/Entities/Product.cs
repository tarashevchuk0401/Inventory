using System.ComponentModel.DataAnnotations.Schema;
using InventoryHub.Domain.Enums;

namespace InventoryHub.Domain.Entities;

public class Product
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }

    public required ProductCategories Category { get; set; }
    public required Guid SupplierId { get; set; }

    [ForeignKey(nameof(SupplierId))]
    public Supplier Supplier { get; set; }

    // public Guid CategoryId { get; set; }

    public static Product Create(
        string name,
        decimal price,
        int stockQuantity,
        Guid supplierId,
        ProductCategories category
    )
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            StockQuantity = stockQuantity,
            SupplierId = supplierId,
            Category = category
        };
    }
}
