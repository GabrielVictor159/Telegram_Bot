using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Services;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Telegram.BOT.WebMVC.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationService notifications;

        public NotificationFilter(INotificationService notifications)
        {
            this.notifications = notifications;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (notifications.HasNotifications)
            {
                var serializeObject = JsonConvert.SerializeObject(notifications.Notifications);
                Environment.SetEnvironmentVariable("Notifications", serializeObject);
                context.HttpContext.Response.Redirect($"/notification");
                return;
            }
            await next();
        }
    }
}
