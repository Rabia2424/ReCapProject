using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        //ICartService _cartService;
        IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            //_cartService = cartService;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExistsControlForRegister(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var userToRegister = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            //var findUser = _userService.GetByEmail(userForRegisterDto.Email);

            var result = _authService.CreateAccessToken(userToRegister.Data);
            if (result.Success)
            {
                //_cartService.InitializeCart(userToRegister.Data.Id);
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("updatepassword")]
        public IActionResult UpdatePassword(UserForPasswordDto userForPasswordDto)
        {
            var resultForUpdatePassword = _authService.UpdatePassword(userForPasswordDto, userForPasswordDto.NewPassword);
            if(resultForUpdatePassword.Success)
            {
                var result = _authService.CreateAccessToken(resultForUpdatePassword.Data);
                if(result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            else
            {
                return BadRequest(resultForUpdatePassword);
            }
        }
    }
}
