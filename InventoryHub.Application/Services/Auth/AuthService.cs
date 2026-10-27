using InventoryHub.Application.Dto;
using InventoryHub.Application.Exceptions;
using InventoryHub.Application.Repositories;
using InventoryHub.Application.Services.Hasher;
using InventoryHub.Application.Services.JWT;
using InventoryHub.Domain.Entities;

namespace InventoryHub.Application.Services.Auth;

public class AuthService(
    IUserRepository usersRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    public async Task<UserDto> SignUp(CreateUserDto userDto)
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

    public async Task<string> SignIn(SignInDto dto)
    {
        var user = await usersRepository.GetUserByEmail(dto.Email);
        if (user is null || !passwordHasher.Compare(dto.Password, user.HashedPassword))
        {
            throw new UnauthorizedException("Email or password is wrong");
        }

        return jwtTokenGenerator.CreateToken(user);
    }
}
