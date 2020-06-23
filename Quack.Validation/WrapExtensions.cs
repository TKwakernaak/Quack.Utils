using System;
using System.Collections.Generic;
using System.Text;
using Quack.Validation.Contracts;

namespace Quack.Validation
{
    public static class WrapExtensions
    {

        /// <summary>
        ///  Wrap T into ValidatableObject<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="rules"></param>
        public static ValidatableObject<T> Wrap<T>(this T input, params IValidationRule<T>[] rules)
        {
            var validatableObject = new ValidatableObject<T>();
            validatableObject.Value = input;

            if (rules != null)
            {
                foreach (var rule in rules)
                {
                    validatableObject.AddValidation(() => rule);
                }
            }

            return validatableObject;
        }
    }
}

