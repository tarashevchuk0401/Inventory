namespace InventoryHub.Application.Services.Hasher;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Compare(string password, string hashedPassword);
}
