using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quack.Validation.Contracts;
using Quack.Validation.ValidatableObject.Contracts;

namespace Quack.Validation
{
    public class ValidatableObject<T> : IValidity
    {
        public List<IValidationRule<T>> Validations = new List<IValidationRule<T>>();
        public List<string> Errors { get; private set; } = new List<string>();

        public ValidatableObject<T> AddValidation(Func<IValidationRule<T>> rule)
        {
            Validations.Add(rule());
            return this;
        }

        public T Value { get; set; }

        public bool IsValid { get; private set; } = true;


        public bool Validate()
        {
            Errors.Clear();

            Errors = Validations.Where(v => !v.Check(Value))
                                .Select(v => v.ValidationMessage)
                                .ToList();

            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}
