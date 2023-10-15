using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.UseCases.User.Forbidden {
    [AllowAnonymous]
    public class UserController : Controller {
        public IActionResult Forbidden() {
            return View();
        }
    }
}
