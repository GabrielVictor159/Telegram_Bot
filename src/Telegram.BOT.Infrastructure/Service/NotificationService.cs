using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using FluentValidator;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        public List<Notification> Notifications { get; set; } = new();
        public bool HasNotifications => Notifications.Any();
        public void AddNotification(string key, string message)
            => Notifications.Add(new Notification(key, message));
        public void AddNotifications(ValidationResult? validationResult)
            => validationResult?.Errors.ToList().ForEach(error => AddNotification(error.ErrorCode, error.ErrorMessage));
    }
}