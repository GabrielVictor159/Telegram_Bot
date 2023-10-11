using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.GetEnv {
    public class EnvVariablesController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
