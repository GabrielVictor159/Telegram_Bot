using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.WebApi.Filters
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
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";
                var obj = JsonConvert.SerializeObject(notifications.Notifications);
                await context.HttpContext.Response.WriteAsync(obj);
                return;
            }
            await next();
        }
    }
}