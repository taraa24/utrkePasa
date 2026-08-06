using UtrkePasa.Api.Dtos;

namespace UtrkePasa.Api.Services;

public interface IUserService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
}