using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Telegram.BOT.Application.Interfaces.Services;
using System.Net;

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
                var urlHelper = new UrlHelper(context);
                var url = urlHelper.Action("ShowNotifications", "Notification", new { notifications = notifications.Notifications });
                context.Result = new RedirectResult(url!);
                return;
            }

            await next();
        }
    }
}
