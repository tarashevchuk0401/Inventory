namespace InventoryHub.Application.Dto.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Guid SupplierId { get; set; }
    public Guid CategoryId { get; set; }
}
