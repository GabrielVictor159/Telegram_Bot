using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.WebMVC.Controllers {
    public class GroupController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() {
            var productGroups = new List<ProductGroups>();
            return View(productGroups);
        }

        public IActionResult Edit() {
            var productGroups = new List<ProductGroups>();
            return View(productGroups); ;
        }
    }
}
