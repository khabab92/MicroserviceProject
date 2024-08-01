using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApp.Models.Dto;
using WebApp.Services.IService;
using WebApp.Utility;

namespace WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;

        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            if (ModelState.IsValid)
            {
                ResponseDto responseDto = await _authService.LoginAsync(obj);
              
                if (responseDto.IsSuccess && responseDto != null)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                    await SignToUser(loginResponseDto);

                    _tokenProvider.SetToken(loginResponseDto.Token);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", responseDto.Message);
                    return View(obj);
                }
            }
            ModelState.AddModelError("CustomError", "");
            return View(obj);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index","Home");
        }
        private async Task SignToUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claimIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u=> u.Type == JwtRegisteredClaimNames.Email).Value));
            claimIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            claimIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            claimIdentity.AddClaim(new Claim(ClaimTypes.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            //get role
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));
            var principal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                 new SelectListItem{Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.roleList = roleList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationRequestDto obj)
        {
            if (ModelState.IsValid)
            {
                ResponseDto result = await _authService.RegisterAsync(obj);
                ResponseDto roleResp;
                if (result.IsSuccess && result != null)
                {
                    if (string.IsNullOrEmpty(obj.roleName))
                    {
                        obj.roleName = SD.RoleCustomer;
                    }
                    roleResp = await _authService.AsignRoleAsync(obj);
                    if (roleResp != null && roleResp.IsSuccess)
                    {
                        TempData["success"] = "Registration Successfuly";
                        return RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    TempData["error"] = result.Message;
                }
            }
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                 new SelectListItem{Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };
           
            ViewBag.roleList = roleList;
            return View();
        }
    }
}
