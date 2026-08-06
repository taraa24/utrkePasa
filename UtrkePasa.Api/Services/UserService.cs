
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
        var user = await _userRepository.GetByEmailAsync(request.email);

        if(user is null || user.password != request.password) return null;

        return new LoginResponseDto
        {
            user_Id = user.user_Id,
            name = user.name,
            surname = user.surname
        };
    }
}