using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.Controllers {
    public class MarcController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() {
            return View();
        }

        [HttpGet]
        public IActionResult Edit() {
            return View();
        }
    }
}
