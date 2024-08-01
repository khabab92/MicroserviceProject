using WebApp.Models.Dto;
using WebApp.Services.IService;
using WebApp.Utility;

namespace WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> AsignRoleAsync(RegistrationRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Post,
                Data = registerRequestDto,
                Url = SD.AuthApiBaseUrl + "/api/auth/asignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Post,
                Data = loginRequestDto,
                Url = SD.AuthApiBaseUrl + "/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.Post,
                Data = registrationRequestDto,
                Url = SD.AuthApiBaseUrl + "/api/auth/register"
            }, withBearer: false);
        }
    }
}
