using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto.Customer;

public class CreateCustomerDto
{
    [Required, MinLength(1)] public string FirstName { get; set; }

    [Required, MinLength(1)] public string LastName { get; set; }

    [Required, EmailAddress] public string Email { get; set; }
}
