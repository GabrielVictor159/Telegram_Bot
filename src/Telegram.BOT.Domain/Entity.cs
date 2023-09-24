using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Telegram.BOT.Domain
{
    public abstract class Entity<TModel, TValidator> : ICloneable
    where TValidator : AbstractValidator<TModel>
    {
        protected TValidator Validator { get; private set; }
        public ValidationResult? ValidationResult { get; private set; }
        public bool IsValid
        {
            get
            {
                ValidationResult = Validator.Validate((TModel)Clone());
                return ValidationResult.IsValid;
            }
            set { }
        }
        protected Entity( TValidator validator)
        {
            Validator = validator;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}