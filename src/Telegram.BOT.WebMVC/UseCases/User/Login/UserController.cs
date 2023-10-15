using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases.Ambient.EnvVariables.GetEnv;

namespace Telegram.BOT.WebMVC.UseCases.User.Login
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IPasswordCompareService passwordCompareService;
        private readonly IGetEnvRequest getEnvRequest;

        public UserController(IPasswordCompareService passwordCompareService, IGetEnvRequest getEnvRequest)
        {
            this.passwordCompareService = passwordCompareService;
            this.getEnvRequest = getEnvRequest;
        }

        public IActionResult Login()
        {
            return View(new UserLoginResponse());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string password)
        {
            var getPassRequest = new Application.UseCases.Ambient.EnvVariables.GetEnv.GetEnvRequest() { Key = "AdminPass" };
            await getEnvRequest.Execute(getPassRequest);

            if(!getPassRequest.IsError && getPassRequest.output != null) {
                string hashedPassword = getPassRequest.output.Value;
                if(passwordCompareService.VerifyPassword(password, hashedPassword)) {
                    var authProperties = new AuthenticationProperties() { 
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = false
                    };
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Role, "Administrador")
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);
                    return LocalRedirect("/");
                } else {
                    return View(new UserLoginResponse() { IsError = true, ErrorMessage = "Senha incorreta"});
                }
            } else {
                return View("Error", (getPassRequest.ErrorMessage, "Login"));
            }
        }

    }
}
