using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.WebMVC.UseCases.Notification
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            var notificationsSerielize = Environment.GetEnvironmentVariable("Notifications");

            if (notificationsSerielize != null)
            {
                var notifications = JsonConvert.DeserializeObject<List<FluentValidator.Notification>>(notificationsSerielize);
                Environment.SetEnvironmentVariable("Notifications", null);
                return View("Index", notifications);
            }
            return View();
        }
    }
}
