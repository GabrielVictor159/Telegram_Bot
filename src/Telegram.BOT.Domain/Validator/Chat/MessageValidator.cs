using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Telegram.BOT.Domain.Chat;

namespace Telegram.BOT.Domain.Validator.Chat
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(p=>p.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Id field is required.");
            RuleFor(p=>p.Messaging)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Messaging field is required.");
            RuleFor(p=>p.ChatId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The ChatId field is required.");
            RuleFor(p => p.NumberMessage)
                .NotEmpty()
                .NotNull()
                .WithMessage("The NumberMessage field is required.");
        }
    }
}