using Order.Repository.AuthRepository.Dtos;

namespace Order.Repository.AuthRepository
{
    public interface IAuthrepository
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LogInDto logInDto);
    }
}
