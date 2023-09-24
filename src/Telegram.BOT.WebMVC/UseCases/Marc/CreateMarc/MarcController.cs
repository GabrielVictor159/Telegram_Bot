using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.WebMVC.UseCases.Marc.GetMarc;

namespace Telegram.BOT.WebMVC.UseCases.Marc.CreateMarc
{
    public class MarcController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
