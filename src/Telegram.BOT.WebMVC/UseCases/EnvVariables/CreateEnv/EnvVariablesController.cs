using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.CreateEnv {
    public class EnvVariablesController : Controller {
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAction() {
            throw new NotImplementedException();
        }
    }
}
