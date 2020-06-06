using System;
using System.Collections.Generic;
using System.Text;
using Quack.Validation.Contracts;

namespace Quack.Validation.Rules
{
    public class IsNotNullOrEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get; private set; }

        public bool Check(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public IsNotNullOrEmptyRule WithValidationMessage(string message)
        {
            ValidationMessage = message;
            return this;
        }
    }
}

