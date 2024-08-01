using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AuthAPI.Models.Dto;
using Services.AuthAPI.Services.IServices;

namespace Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _responseDto;

        public AuthAPIController(IAuthService authService)
        {
            _authService=authService;
            _responseDto = new ();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrer([FromBody] RegistrationRequestDto model )
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(model);
                if (!string.IsNullOrEmpty(result))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = result;
                    return BadRequest(_responseDto);
                }
                return Ok(_responseDto);
            }
            
            return BadRequest(_responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var loginResponse =await _authService.Login(model);
                if(loginResponse.User == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Invalide Username or Password!";
                    return BadRequest(_responseDto);
                }
                _responseDto.Result = loginResponse;
                return Ok(_responseDto);
            }
            return BadRequest(_responseDto);
        }
        [HttpPost("asignRole")]
        public async Task<IActionResult> AsignRole([FromBody] RegistrationRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var roleResponse = await _authService.AsignRole(model.Email,model.roleName);
                if (!roleResponse)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "user can't asigning to role!!";
                    return BadRequest(_responseDto);
                }
               
                return Ok(_responseDto);
            }
            return BadRequest(_responseDto);
        }
    }
}
