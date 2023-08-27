using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;
using Telegram.BOT.Application.UseCases;
using Telegram.BOT.Application.UseCases.Order.CreateOrder;

namespace Telegram.BOT.Application.UseCases.Order.CreateOrder.Handlers
{
    public class ValidateOrderHandler : Handler<CreateOrderRequest>
    {
        private readonly INotificationService _notificationService;
        public ValidateOrderHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public override async Task ProcessRequest(CreateOrderRequest request)
        {
            if (!request.Order.IsValid)
            {
                _notificationService.AddNotifications(request.Order.ValidationResult);
                return;
            }
            if (sucessor != null)
            {
                await sucessor.ProcessRequest(request);
            }
        }
    }
}