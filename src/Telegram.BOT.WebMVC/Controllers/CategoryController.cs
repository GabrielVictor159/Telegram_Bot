using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.WebMVC.Controllers {
    public class CategoryController : Controller {
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
