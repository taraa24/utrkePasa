
using UtrkePasa.Api.Dtos;
using UtrkePasa.Domain.Repository;

namespace UtrkePasa.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if(user is null || user.Password != request.Password) return null;

        return new LoginResponseDto
        {
            UserId = user.UserId,
            Name = user.Name,
            Surname = user.Surname
        };
    }
}