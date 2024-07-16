using Microsoft.AspNetCore.Identity;
using Order.Repository.AuthRepository.Dtos;
using Order.Repository.Entities;
using Order.Repository.TokenServices;

namespace Order.Repository.AuthRepository
{
    public class AutheRepository : IAuthrepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenServices _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AutheRepository(UserManager<AppUser> userManager, ITokenServices tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        #region Register Service
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
                return null;

            var appUser = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
                return null;
            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            };
        }
        #endregion

        #region LogIn Service
        public async Task<UserDto> Login(LogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        #endregion

    }
}
