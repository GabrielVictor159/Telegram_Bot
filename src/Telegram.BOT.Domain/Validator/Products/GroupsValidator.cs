using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Domain.Validator.Products
{
    public class GroupsValidator : AbstractValidator<Groups>
    {
        public GroupsValidator() 
        {
            RuleFor(group => group.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Id field is required.");
            RuleFor(group => group.Tags)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Tags field is required.");
            RuleFor(group => group.CreateDate)
                .NotEmpty()
                .NotNull()
                .WithMessage("The CreateDate field is required.");
        }
    }
}
