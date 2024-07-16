using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.APi.HandleResponse;
using Order.Repository.AuthRepository;
using Order.Repository.AuthRepository.Dtos;
using Order.Repository.Entities;
using System.Security.Claims;

namespace Order.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthrepository _userServices;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IAuthrepository userServices, UserManager<AppUser> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }

        #region Register EndPoint
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userServices.Register(registerDto);
            if (user == null)
                return BadRequest(new ApiException(400, "This Email Is Already Exist"));

            return Ok(user);
        }
        #endregion

        #region LogIn EndPoint
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LogInDto logInDto)
        {
            var user = await _userServices.Login(logInDto);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            return Ok(user);
        }
        #endregion

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email
            };
        }

    }
}
