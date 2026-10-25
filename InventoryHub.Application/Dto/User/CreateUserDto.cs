using System.ComponentModel.DataAnnotations;

namespace InventoryHub.Application.Dto;

public class CreateUserDto
{
    [Required, MinLength(1)] public string FirstName { get; set; }

    [Required,  MinLength(1)]    public  string LastName { get; set; }

    [Required, EmailAddress]  public string Email { get; set; }

    [Required, MinLength(4), MaxLength(64)]  public string Password { get; set; }
}
