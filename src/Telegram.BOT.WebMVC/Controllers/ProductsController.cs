using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Repositories;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.WebMVC.Controllers {
    public class ProductsController : Controller {

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            var marc = new List<Marc>();
            return View(marc);
        }

        [HttpGet]
        public IActionResult Edit() {
            var marc = new List<Marc>();
            return View(marc);
        }
    }
}
