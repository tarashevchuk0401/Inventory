using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services.JWT;

public interface IJwtTokenGenerator
{
    public string CreateToken(User user);
}
