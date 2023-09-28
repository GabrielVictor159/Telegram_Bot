using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Domain.Products;

namespace Telegram.BOT.Domain.Validator.Products;

public class ProductGroupsValidator : AbstractValidator<ProductGroups>
{
    public ProductGroupsValidator()
    {
        RuleFor(group => group.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("The Id field is required.");
        RuleFor(group => group.ProductId)
            .NotEmpty()
            .NotNull()
            .WithMessage("The ProductId field is required.");
        RuleFor(group => group.GroupId)
            .NotEmpty()
            .NotNull()
            .WithMessage("The GroupId field is required.");
        RuleFor(group => group.Percentage)
            .NotEmpty()
            .NotNull()
            .WithMessage("The Percentage field is required.");
    }
}
