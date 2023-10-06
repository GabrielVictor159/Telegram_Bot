using FluentValidator;
using Microsoft.AspNetCore.Mvc;

namespace Telegram.BOT.WebMVC.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowNotifications(List<Notification> notifications)
        {
            return View(notifications);
        }
    }
}
