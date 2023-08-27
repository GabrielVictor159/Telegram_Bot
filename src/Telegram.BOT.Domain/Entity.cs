using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Telegram.BOT.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public bool IsValid { get; private set; }
        public ValidationResult? ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);

            return IsValid = ValidationResult.IsValid;
        }
    }
}