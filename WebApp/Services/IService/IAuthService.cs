using WebApp.Models.Dto;

namespace WebApp.Services.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto> AsignRoleAsync(RegistrationRequestDto registerRequestDto);
    }
}
