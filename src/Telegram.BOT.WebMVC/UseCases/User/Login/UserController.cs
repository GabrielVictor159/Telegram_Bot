using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.WebMVC.UseCases.User.Login
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IPasswordHashService passwordHashService;

        public UserController(IPasswordHashService passwordHashService)
        {
            this.passwordHashService = passwordHashService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string password)
        {
            throw new NotImplementedException();
        }

    }
}
