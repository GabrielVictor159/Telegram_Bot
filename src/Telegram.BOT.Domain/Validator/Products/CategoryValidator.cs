using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Domain.Validator.Products
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p=>p.Id)
            .NotEmpty().NotNull()
            .WithMessage("The Id field is required.");
            RuleFor(p=>p.Name)
            .NotNull().NotEmpty()
            .WithMessage("The Name field is required.")
            .MinimumLength(5)
            .WithMessage("The Name field must have more than 5 characters.");
        }
        
    }
}