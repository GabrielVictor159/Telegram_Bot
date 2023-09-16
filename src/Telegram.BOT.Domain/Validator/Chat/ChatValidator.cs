using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Telegram.BOT.Domain.Validator.Chat
{
    public class ChatValidator : AbstractValidator<Domain.Chat.Chat>
    {
        public ChatValidator()
        {
            RuleFor(p=>p.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Id field is required.");
            RuleFor(p=>p.CreateDateTime)
                .NotNull()
                .NotEmpty()
                .WithMessage("The CreateDateTime field is required.");
        }
    }
}