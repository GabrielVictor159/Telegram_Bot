using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.UseCases.EnvVariables.UpdateEnv {
    public class EnvVariablesController : Controller {
        public IActionResult Edit() {
            return View();
        }

        [HttpPost]
        public IActionResult EditAction() {
            throw new NotImplementedException();
        }
    }
}
