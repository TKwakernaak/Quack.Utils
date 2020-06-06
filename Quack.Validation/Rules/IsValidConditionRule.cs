using System;
using System.Collections.Generic;
using System.Text;
using Quack.Validation.Contracts;

namespace Quack.Validation.Rules
{
    public class IsValidConditionRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; private set; }

        private Func<T, bool> _validationRule { get; set; }

        public IsValidConditionRule<T> Ensure(Func<T, bool> rule)
        {
            if (_validationRule != null)
                throw new ArgumentException("Ensure can only be set once per rule. ");

            _validationRule = rule;
            return this;
        }

        public IsValidConditionRule<T> SetValidationMessage(string message)
        {
            ValidationMessage = message;
            return this;
        }

        /// <summary>
        /// if func evaluates to true, the item should fail & visa versa
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Check(T value)
        {
            return _validationRule(value);
        }
    }
}