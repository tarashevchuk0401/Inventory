using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto.Supplier;

public class CreateSupplierDto
{
    [Required, MinLength(1)] public string Name { get; set; }

    [Required, EmailAddress] public string Email { get; set; }
}
