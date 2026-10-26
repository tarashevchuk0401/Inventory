using InventoryHub.Application.Dto;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Application.Services.Hasher;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services;

public class UserService(IUserRepository usersRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<List<UserDto>> GetAllAsync()
    {
        var all = await usersRepository.GetAllAsync();

        return all
            .Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role.ToString()
            })
            .ToList();
    }

    public async Task<UserDto> GetUser(Guid id)
    {
        var user = await usersRepository.GetUser(id) ?? throw new NotFoundException("No User found");

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<UserDto> CreateUser(CreateUserDto userDto)
    {
        var isEmailExist = await usersRepository.ExistsByEmailAsync(userDto.Email);
        if (isEmailExist)
        {
            throw new ConflictException("EMAIL already occupied");
        }

        var hashedPassword = passwordHasher.Hash(userDto.Password);

        var user = User.Create(userDto.FirstName, userDto.LastName, userDto.Email, hashedPassword);
        await usersRepository.AddAsync(user);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = user.Role
        };
    }
}
