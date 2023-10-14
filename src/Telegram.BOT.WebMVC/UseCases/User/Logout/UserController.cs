using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.UseCases.User.Logout {
    public class UserController : Controller {

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return LocalRedirect("/User/Login");
        }
    }
}
