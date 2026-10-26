using InventoryHub.Application.Dto;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services.Auth;

public class AuthService(IUserRepository usersRepository) : IAuthService
{
    public async Task<UserDto> SignUp(CreateUserDto userDto)
    {
        var isEmailExist = await usersRepository.ExistsByEmailAsync(userDto.Email);
        if (isEmailExist)
        {
            throw new ConflictException("EMAIL already occupied");
        }

        var user = User.Create(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password);
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

    public async Task<string> SignIn(SignInDto dto)
    {
        var isValid = await usersRepository.CheckPassword(dto.Email, dto.Password);
        if (!isValid)
        {
            throw new UnauthorizedException("Email or password is wrong");
        }

        return "Hi";
    }
}
