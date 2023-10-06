using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.WebMVC.Controllers {

    public class UserController : Controller {
        private readonly IPasswordHashService passwordHashService;

        public UserController(IPasswordHashService passwordHashService) {
            this.passwordHashService = passwordHashService;
        }

        [AllowAnonymous]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string password) {
            throw new NotImplementedException();
        }

        public IActionResult Logout() {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        public IActionResult ChangePassword() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangePassword(string oldPassword, string newPassword) {  
            throw new NotImplementedException(); 
        }
    }
}
