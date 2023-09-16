using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Domain.Validator.Products
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Id field is required.");
            RuleFor(p=>p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Name field is required.");
            RuleFor(p => p.Tags)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Tags field is required.")
                .Must(tags => !string.IsNullOrWhiteSpace(tags) && tags.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length > 5)
                .WithMessage("The Tags field must have more than 5 words.");
            RuleFor(p=>p.Price)
                .NotNull()
                .WithMessage("The Price field is required.")
                .Must(p=> p>0)
                .WithMessage("The Price field must be greater than 0.");
            RuleFor(p=>p.MarcId)
                .NotNull()
                .NotEmpty()
                .WithMessage("The MarcId field is required.");
        }
    }
}
